using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocument.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;

namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocumentDetails.Response
{
    public class DocumentDetailsDto
    {
        public string SubmissionUUID { get; set; }
        public string LongId { get; set; }
        public DateTime DateTimeReceived { get; set; }
        public string Status { get; set; }
        public string TransformationStatus { get; set; }
        public DocumentValidationResultsDto ValidationResults { get; set; }
        public DocumentDto Document { get; set; }
        public DateTime? CancelRequestDate { get; set; }
        public DateTime? RejectRequestDate { get; set; }
        public DateTime? CancelRequestDelayedDate { get; set; }
        public DateTime? RejectRequestDelayedDate { get; set; }
        public DateTime? DeclineCancelRequestDate { get; set; }
        public DateTime? DeclineRejectRequestDate { get; set; }
        public FreezeStatusDto FreezeStatus { get; set; }
        public DocumentAdditionalMetadataDto[] AdditionalMetadata { get; set; }
        public string LateSubmissionRequestNumber { get; set; }
    }
}
