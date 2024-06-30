using BuildingBlocks.Results;

namespace EgyptianeInvoicing.Core.Services.Abstractions
{
    public interface ITokenSigner
    {
        Result<string> SignDocuments(string serializedJson, string tokenPin, string certificate);
    }
}
