namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Response
{
    public class SubmissionResponseDto
    {
        public string submissionId { get; set; }
        public List<DocumentAcceptedDto> acceptedDocuments { get; set; }
        public List<DocumentRejectedDto> rejectedDocuments { get; set; }
    }
}
