using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.DocumentsType.Response;

namespace EgyptianeInvoicing.Core.Clients.Common.Abstractions
{
    public interface IDocumentTypesClient
    {
        Task<List<DocumentTypeDto>> GetDocumentTypesAsync();
        Task<DocumentTypeDto> GetDocumentTypeAsync( int documentTypeId);
        Task<DocumentTypeVersionDto> GetDocumentTypeVersionAsync( int documentTypeId, int documentTypeVersionId);
    }
}
