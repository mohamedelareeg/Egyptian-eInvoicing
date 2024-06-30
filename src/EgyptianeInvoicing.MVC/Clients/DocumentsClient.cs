using EgyptianeInvoicing.Shared.Requests;
using EgyptianeInvoicing.MVC.Base;
using EgyptianeInvoicing.MVC.Clients.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response;
using EgyptianeInvoicing.MVC.Constants;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Response;
using EgyptianeInvoicing.Shared.Dtos;
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
        public async Task<BaseResponse<byte[]>> GetDocumentPackageAsync(string rid)
        {
            var CompanyId = CompanyDtoSingleton.Instance?.ReferenceId;

            var request = new { Rid = rid };
            var response = await PostAsync<object, BaseResponse<byte[]>>($"api/v1/documents/package/{rid}?CompanyId={CompanyId}", request);
            return response;
        }

        public async Task<BaseResponse<byte[]>> GetDocumentPDFAsync(string rid)
        {
            var CompanyId = CompanyDtoSingleton.Instance?.ReferenceId;
            var request = new { Rid = rid };
            var response = await PostAsync<object, BaseResponse<byte[]>>($"api/v1/documents/pdf/{rid}?CompanyId={CompanyId}", request);
            return response;
        }
        public async Task<BaseResponse<SubmissionResponseDto>> SubmitInvoiceAsync(Guid CompanyId, List<ImportedInvoiceDto> invoices)
        {
            var request = new SubmitInvoiceRequestDto { CompanyId = CompanyId, Invoices = invoices };
            var response = await PostAsync<SubmitInvoiceRequestDto, BaseResponse<SubmissionResponseDto>>("api/v1/documents/submit-invoice", request);
            return response;
        }
        public async Task<Stream> DownloadImportInvoicesAsync()
        {
            var response = await HttpClient.GetAsync("api/v1/documents/download-import-invoices");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to download import_invoices.xlsx. Status code: {response.StatusCode}");
            }

            return await response.Content.ReadAsStreamAsync();
        }
        //public async Task<byte[]> DownloadImportInvoicesAsync()
        //{
        //    var response = await HttpClient.GetAsync("api/v1/documents/download-import-invoices");

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new HttpRequestException($"Failed to download import_invoices.xlsx. Status code: {response.StatusCode}");
        //    }

        //    return await response.Content.ReadAsByteArrayAsync();
        //}

    }
}
