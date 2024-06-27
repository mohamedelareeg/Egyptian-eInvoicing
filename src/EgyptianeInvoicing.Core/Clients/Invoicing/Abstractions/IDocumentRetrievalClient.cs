using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocument.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocumentDetails.Response;

namespace EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions
{
    public interface IDocumentRetrievalClient
    {
        Task<DocumentRetrievalDto> GetDocumentAsync(Guid companyId, string documentUUID);
        Task<byte[]> GetDocumentPdfAsync(Guid companyId, string documentUUID);
        Task<DocumentDetailsDto> GetDocumentDetailsAsync(Guid companyId, string documentUUID);
    }
}
