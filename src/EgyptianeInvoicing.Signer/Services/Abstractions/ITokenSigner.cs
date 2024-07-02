using BuildingBlocks.Results;
using Net.Pkcs11Interop.HighLevelAPI;
using System.Security.Cryptography.X509Certificates;

namespace EgyptianeInvoicing.Signer.Services.Abstractions
{
    public interface ITokenSigner
    {
        Result<string> SignDocuments(string serializedJson, string tokenPin, string certificate, IObjectHandle objectcertificate = null, X509Certificate2 certForSigning = null);
        Result<SigningResultDto> SignDocuments(string tokenPin, string certificate);
    }
}
