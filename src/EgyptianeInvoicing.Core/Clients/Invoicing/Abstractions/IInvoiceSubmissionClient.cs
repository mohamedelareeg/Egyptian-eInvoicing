using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Response;

namespace EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions
{
    public interface IInvoiceSubmissionClient
    {
        Task<SubmissionResponseDto> SubmitRegularInvoiceAsync(List<DocumentDto> request);
        Task<SubmissionResponseDto> SubmitDebitNoteAsync(List<DocumentDto> request);
        Task<SubmissionResponseDto> SubmitCreditNoteAsync(List<DocumentDto> request);
        Task<SubmissionResponseDto> SubmitExportInvoiceAsync(List<DocumentDto> request);
        Task<SubmissionResponseDto> SubmitExportDebitNoteAsync(List<DocumentDto> request);
        Task<SubmissionResponseDto> SubmitExportCreditNoteAsync(List<DocumentDto> request);
    }
}
