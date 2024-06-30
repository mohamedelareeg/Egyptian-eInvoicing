using BuildingBlocks.Results;
using EgyptianeInvoicing.Shared.Dtos.SignerDto;
using EgyptianeInvoicing.Signer.Services.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EgyptianeInvoicing.Signer.Services
{
    public class TokenSigningService : ISigningService
    {

        private readonly ILogger<TokenSigningService> _logger;

        public TokenSigningService(ILogger<TokenSigningService> logger)
        {
            _logger = logger;
        }
        public Result<string> SignWithCMS(string serializedJson ,TokenSigningSettingsDto tokenSigningSettings, string tokenPin, string certificate)
        {
            Result<bool> validationResult = ValidateInputs(serializedJson, tokenSigningSettings.DllLibPath, tokenPin, certificate);
            if (validationResult.IsFailure)
                return Result.Failure<string>(validationResult.Error);

            byte[] data = Encoding.UTF8.GetBytes(serializedJson);
            Result<IPkcs11Library> pkcs11LibraryResult = LoadPkcs11Library(tokenSigningSettings.DllLibPath);
            if (pkcs11LibraryResult.IsFailure)
                return Result.Failure<string>(pkcs11LibraryResult.Error);

            Result<ISlot> slotResult = GetFirstAvailableSlot(pkcs11LibraryResult.Value);
            if (slotResult.IsFailure)
                return Result.Failure<string>(slotResult.Error);

            Result<ISession> sessionResult = OpenSession(slotResult.Value);
            if (sessionResult.IsFailure)
                return Result.Failure<string>(sessionResult.Error);

            Result<Unit> loginResult = Login(sessionResult.Value, tokenPin);
            if (loginResult.IsFailure)
                return Result.Failure<string>(loginResult.Error);

            Result<IObjectHandle> certificateResult = FindCertificate(sessionResult.Value);
            if (certificateResult.IsFailure)
                return Result.Failure<string>(certificateResult.Error);

            Result<X509Certificate2> certForSigningResult = FindCertificateInStore(certificate);
            if (certForSigningResult.IsFailure)
                return Result.Failure<string>(certForSigningResult.Error);

            Result<SignedCms> cmsResult = CreateSignedCms(data, certificateResult.Value, certForSigningResult.Value);
            if (cmsResult.IsFailure)
                return Result.Failure<string>(cmsResult.Error);

            Result<byte[]> encodedCmsResult = ComputeSignature(cmsResult.Value);
            if (encodedCmsResult.IsFailure)
                return Result.Failure<string>(encodedCmsResult.Error);

            return Result.Success(Convert.ToBase64String(encodedCmsResult.Value));
        }

        private Result<bool> ValidateInputs(string serializedJson, string dllLibPath, string tokenPin, string tokenCertificate)
        {
            if (string.IsNullOrEmpty(serializedJson))
                return Result.Failure<bool>(new Error("TokenSigningService.ValidateInputs", "Serialized JSON is null or empty"));

            if (string.IsNullOrEmpty(dllLibPath))
                return Result.Failure<bool>(new Error("TokenSigningService.ValidateInputs", "DLL library path is null or empty"));

            if (string.IsNullOrEmpty(tokenPin))
                return Result.Failure<bool>(new Error("TokenSigningService.ValidateInputs", "Token PIN is null or empty"));

            if (string.IsNullOrEmpty(tokenCertificate))
                return Result.Failure<bool>(new Error("TokenSigningService.ValidateInputs", "Token certificate is null or empty"));

            return Result.Success(true);
        }


        private Result<IPkcs11Library> LoadPkcs11Library(string dllLibPath)
        {
            try
            {
                Pkcs11InteropFactories factories = new Pkcs11InteropFactories();
                IPkcs11Library pkcs11Library = factories.Pkcs11LibraryFactory.LoadPkcs11Library(factories, dllLibPath, AppType.MultiThreaded);
                return Result.Success(pkcs11Library);
            }
            catch (Exception ex)
            {
                return Result.Failure<IPkcs11Library>(new Error("TokenSigningService.LoadPkcs11Library", ex.Message));
            }
        }

        private Result<ISlot> GetFirstAvailableSlot(IPkcs11Library pkcs11Library)
        {
            try
            {
                ISlot slot = pkcs11Library.GetSlotList(SlotsType.WithTokenPresent).FirstOrDefault();
                if (slot == null)
                    return Result.Failure<ISlot>(new Error("TokenSigningService.GetFirstAvailableSlot", "No slots found"));

                return Result.Success(slot);
            }
            catch (Exception ex)
            {
                return Result.Failure<ISlot>(new Error("TokenSigningService.GetFirstAvailableSlot", ex.Message));
            }
        }

        private Result<ISession> OpenSession(ISlot slot)
        {
            try
            {
                ISession session = slot.OpenSession(SessionType.ReadWrite);
                return Result.Success(session);
            }
            catch (Exception ex)
            {
                return Result.Failure<ISession>(new Error("TokenSigningService.OpenSession", ex.Message));
            }
        }

        private Result<Unit> Login(ISession session, string tokenPin)
        {
            try
            {
                CKS sessionState = session.GetSessionInfo().State;
                if (sessionState == CKS.CKS_RO_USER_FUNCTIONS || sessionState == CKS.CKS_RW_USER_FUNCTIONS)
                {
                    _logger.LogInformation("Session is already logged in.");
                    return Result.Success(Unit.Value);
                }
                session.Login(CKU.CKU_USER, Encoding.UTF8.GetBytes(tokenPin));
                return Result.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result.Failure<Unit>(new Error("TokenSigningService.Login", ex.Message));
            }
        }

        private Result<IObjectHandle> FindCertificate(ISession session)
        {
            try
            {
                var certificateSearchAttributes = new List<IObjectAttribute>()
        {
            session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE),
            session.Factories.ObjectAttributeFactory.Create(CKA.CKA_TOKEN, true),
            session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CERTIFICATE_TYPE, CKC.CKC_X_509)
        };

                IObjectHandle certificate = session.FindAllObjects(certificateSearchAttributes).FirstOrDefault();
                if (certificate == null)
                    return Result.Failure<IObjectHandle>(new Error("TokenSigningService.FindCertificate", "Certificate not found"));

                return Result.Success(certificate);
            }
            catch (Exception ex)
            {
                return Result.Failure<IObjectHandle>(new Error("TokenSigningService.FindCertificate", ex.Message));
            }
        }

        private Result<X509Certificate2> FindCertificateInStore(string tokenCertificate)
        {
            try
            {
                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.MaxAllowed);
                var foundCerts = store.Certificates.Find(X509FindType.FindByIssuerName, tokenCertificate, false);
                store.Close();

                if (foundCerts.Count == 0)
                    return Result.Failure<X509Certificate2>(new Error("TokenSigningService.FindCertificateInStore", "No matching certificate found"));

                return Result.Success(foundCerts[0]);
            }
            catch (Exception ex)
            {
                return Result.Failure<X509Certificate2>(new Error("TokenSigningService.FindCertificateInStore", ex.Message));
            }
        }

        private Result<SignedCms> CreateSignedCms(byte[] data, IObjectHandle certificateHandle, X509Certificate2 certForSigning)
        {
            try
            {
                ContentInfo content = new ContentInfo(new Oid("1.2.840.113549.1.7.5"), data);

                EssCertIDv2 bouncyCertificate = new EssCertIDv2(new Org.BouncyCastle.Asn1.X509.AlgorithmIdentifier(new DerObjectIdentifier("1.2.840.113549.1.9.16.2.47")), this.HashBytes(certForSigning.RawData));
                SigningCertificateV2 signerCertificateV2 = new SigningCertificateV2(new EssCertIDv2[] { bouncyCertificate });

                CmsSigner signer = new CmsSigner(certForSigning);
                signer.DigestAlgorithm = new Oid("2.16.840.1.101.3.4.2.1");
                signer.SignedAttributes.Add(new Pkcs9SigningTime(DateTime.UtcNow));
                signer.SignedAttributes.Add(new AsnEncodedData(new Oid("1.2.840.113549.1.9.16.2.47"), signerCertificateV2.GetEncoded()));

                SignedCms cms = new SignedCms(content, true);
                cms.ComputeSignature(signer);
                return Result.Success(cms);
            }
            catch (Exception ex)
            {
                return Result.Failure<SignedCms>(new Error("TokenSigningService.CreateSignedCms", ex.Message));
            }
        }

        private byte[] ComputeSignature(SignedCms cms)
        {
            return cms.Encode();
        }

        private byte[] HashBytes(byte[] input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                return sha.ComputeHash(input);
            }
        }

    }
}
