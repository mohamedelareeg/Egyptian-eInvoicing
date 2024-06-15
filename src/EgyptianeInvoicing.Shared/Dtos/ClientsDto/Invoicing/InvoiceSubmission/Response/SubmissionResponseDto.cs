namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Response
{
    public class SubmissionResponseDto
    {
        public string SubmissionUUID { get; set; }
        public List<DocumentAcceptedDto> AcceptedDocuments { get; set; }
        public List<DocumentRejectedDto> RejectedDocuments { get; set; }
    }
}
