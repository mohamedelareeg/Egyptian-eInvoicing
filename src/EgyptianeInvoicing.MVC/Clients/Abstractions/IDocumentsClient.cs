using EgyptianeInvoicing.MVC.Base;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response;
using EgyptianeInvoicing.Shared.Requests;
namespace EgyptianeInvoicing.MVC.Clients.Abstractions
{
    public interface IDocumentsClient
    {
        Task<BaseResponse<RecentDocumentsDto>> SearchDocumentsAsync(SearchDocumentsRequestDto request);

    }
}
