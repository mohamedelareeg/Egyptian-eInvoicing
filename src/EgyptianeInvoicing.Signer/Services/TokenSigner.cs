using BuildingBlocks.Results;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using EgyptianeInvoicing.Signer.Services.Abstractions;
using System;
using System.IO;
using EgyptianeInvoicing.Shared.Dtos.SignerDto;
using Microsoft.Extensions.Options;
using Net.Pkcs11Interop.HighLevelAPI;
using System.Security.Cryptography.X509Certificates;

namespace EgyptianeInvoicing.Signer.Services
{
    public class TokenSigner : ITokenSigner
    {
        private readonly ISigningService _signingService;
        private readonly ISerializationService _serializationService;
        private readonly TokenSigningSettingsDto _settings;
        public TokenSigner(
           ISigningService signingService,
           ISerializationService serializationService,
           IOptions<TokenSigningSettingsDto> settings)
        {
            _signingService = signingService;
            _serializationService = serializationService;
            _settings = settings.Value;
        }

        public Result<string> SignDocuments(string serializedJson, string tokenPin, string certificate, IObjectHandle objectcertificate = null, X509Certificate2 certForSigning = null)
        {
            try
            {
                if (string.IsNullOrEmpty(serializedJson))
                {
                    return Result.Failure<string>("SignDocuments", "Serialized JSON data is required.");
                }

                if (string.IsNullOrEmpty(tokenPin))
                {
                    return Result.Failure<string>("SignDocuments", "Token PIN is required.");
                }

                if (string.IsNullOrEmpty(certificate))
                {
                    return Result.Failure<string>("SignDocuments", "Certificate is required.");
                }

                var request = JsonConvert.DeserializeObject<JObject>(serializedJson, new JsonSerializerSettings
                {
                    FloatFormatHandling = FloatFormatHandling.String,
                    FloatParseHandling = FloatParseHandling.Decimal,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateParseHandling = DateParseHandling.None
                });

                var canonicalStringResult = _serializationService.Serialize(request);
                if (canonicalStringResult.IsFailure)
                {
                    return Result.Failure<string>(canonicalStringResult.Error);
                }

                string canonicalString = canonicalStringResult.Value;
                string cadesResult = string.Empty;
                if (objectcertificate is not null && certForSigning is not null)
                {
                    var result = _signingService.UseSignature(canonicalString, objectcertificate, certForSigning);
                    if (result.IsFailure)
                    {
                        return Result.Failure<string>(result.Error);
                    }
                    cadesResult = result.Value;
                }
                else
                {
                    var result = _signingService.SignWithCMS(canonicalString, _settings, tokenPin, certificate);
                    if (result.IsFailure)
                    {
                        return Result.Failure<string>(result.Error);
                    }
                    cadesResult = result.Value;
                }



                return cadesResult;

                //JObject signaturesObject = new JObject(
                //    new JProperty("signatureType", "I"),
                //    new JProperty("value", cades));
                //JArray signaturesArray = new JArray();
                //signaturesArray.Add(signaturesObject);
                //request.Add("signatures", signaturesArray);

                //string fullSignedDocument = "{\"documents\":[" + request.ToString() + "]}";

                //string path = Path.Combine(Directory.GetCurrentDirectory(), "signatures");
                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}

                //string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                //string fullSignedDocumentFilePath = Path.Combine(path, $"FullSignedDocument_{timestamp}.json");

                //File.WriteAllText(fullSignedDocumentFilePath, fullSignedDocument);

                //return Result.Success(fullSignedDocumentFilePath);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(new Error("TokenSigner.SignDocuments", ex.Message));
            }
        }

        public Result<SigningResultDto> SignDocuments(string tokenPin, string certificate)
        {
            try
            {
                var result = _signingService.Sign("eps2003csp11.dll", tokenPin, certificate);
                if (result.IsFailure)
                {
                    return Result.Failure<SigningResultDto>(result.Error);
                }

                return result.Value;

            }
            catch (Exception ex)
            {
                return Result.Failure<SigningResultDto>(new Error("TokenSigner.SignDocuments", ex.Message));
            }
        }
    }
}
