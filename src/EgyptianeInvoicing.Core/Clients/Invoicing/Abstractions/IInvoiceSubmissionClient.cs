using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Response;

namespace EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions
{
    public interface IInvoiceSubmissionClient
    {
        Task<SubmissionResponseDto> SubmitRegularInvoiceAsync(Guid companyId, List<DocumentDto> request);
        Task<SubmissionResponseDto> SubmitDebitNoteAsync(Guid companyId, List<DocumentDto> request);
        Task<SubmissionResponseDto> SubmitCreditNoteAsync(Guid companyId, List<DocumentDto> request);
        Task<SubmissionResponseDto> SubmitExportInvoiceAsync(Guid companyId, List<DocumentDto> request);
        Task<SubmissionResponseDto> SubmitExportDebitNoteAsync(Guid companyId, List<DocumentDto> request);
        Task<SubmissionResponseDto> SubmitExportCreditNoteAsync(Guid companyId, List<DocumentDto> request);
    }
}
