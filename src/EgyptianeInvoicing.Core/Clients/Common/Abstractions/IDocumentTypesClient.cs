using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.DocumentsType.Response;

namespace EgyptianeInvoicing.Core.Clients.Common.Abstractions
{
    public interface IDocumentTypesClient
    {
        Task<List<DocumentTypeDto>> GetDocumentTypesAsync(Guid companyId);
        Task<DocumentTypeDto> GetDocumentTypeAsync(Guid companyId, int documentTypeId);
        Task<DocumentTypeVersionDto> GetDocumentTypeVersionAsync(Guid companyId, int documentTypeId, int documentTypeVersionId);
    }
}
