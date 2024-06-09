using BuildingBlocks.Results;

namespace EgyptianeInvoicing.SignApi.Services.Abstractions
{
    public interface ITokenSigner
    {
        Result<string> SignDocuments(string serializedJson);
    }
}
