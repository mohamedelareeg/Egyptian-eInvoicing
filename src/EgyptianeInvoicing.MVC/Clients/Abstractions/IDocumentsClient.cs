using EgyptianeInvoicing.MVC.Base;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Response;
using EgyptianeInvoicing.Shared.Requests;
namespace EgyptianeInvoicing.MVC.Clients.Abstractions
{
    public interface IDocumentsClient
    {
        Task<BaseResponse<RecentDocumentsDto>> SearchDocumentsAsync(SearchDocumentsRequestDto request);
        Task<BaseResponse<RecentDocumentsDto>> RecentDocumentsAsync(RecentDocumentsRequestDto request);
        Task<BaseResponse<byte[]>> GetDocumentPackageAsync(string Rid);
        Task<BaseResponse<byte[]>> GetDocumentPDFAsync(string Rid);
        Task<BaseResponse<SubmissionResponseDto>> SubmitInvoiceAsync(Guid CompanyId, List<ImportedInvoiceDto> invoices);
        Task<Stream> DownloadImportInvoicesAsync();
    }
}
