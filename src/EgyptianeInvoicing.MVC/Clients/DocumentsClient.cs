using EgyptianeInvoicing.Shared.Requests;
using EgyptianeInvoicing.MVC.Base;
using EgyptianeInvoicing.MVC.Clients.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response;
namespace EgyptianeInvoicing.MVC.Clients
{
    public class DocumentsClient : BaseClient, IDocumentsClient
    {
        public DocumentsClient(IHttpClientFactory httpClientFactory, ILogger<DocumentsClient> logger, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory.CreateClient("ApiClient"), logger, httpContextAccessor)
        {
        }

        public async Task<BaseResponse<RecentDocumentsDto>> SearchDocumentsAsync(SearchDocumentsRequestDto request)
        {
            return await PostAsync<SearchDocumentsRequestDto, BaseResponse<RecentDocumentsDto>>("api/v1/documents/search", request);
        }
        public async Task<BaseResponse<RecentDocumentsDto>> RecentDocumentsAsync(RecentDocumentsRequestDto request)
        {
            return await PostAsync<RecentDocumentsRequestDto, BaseResponse<RecentDocumentsDto>>("api/v1/documents/recent", request);
        }
    }
}
