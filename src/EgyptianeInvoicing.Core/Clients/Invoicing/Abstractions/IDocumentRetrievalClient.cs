using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocument.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocumentDetails.Response;

namespace EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions
{
    public interface IDocumentRetrievalClient
    {
        Task<DocumentRetrievalDto> GetDocumentAsync(string documentUUID);
        Task<byte[]> GetDocumentPdfAsync(string documentUUID);
        Task<DocumentDetailsDto> GetDocumentDetailsAsync(string documentUUID);
    }
}
