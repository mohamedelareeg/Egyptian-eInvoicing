namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response
{
    public class DocumentSummaryDto
    {
        public string Uuid { get; set; }
        public string SubmissionUUID { get; set; }
        public string LongId { get; set; }
        public string PublicUrl { get; set; }
        public string InternalId { get; set; }
        public string TypeName { get; set; }
        public string TypeVersionName { get; set; }
        public string IssuerId { get; set; }
        public string IssuerName { get; set; }
        public string IssuerType { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverType { get; set; }
        public DateTime DateTimeIssued { get; set; }
        public DateTime DateTimeReceived { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public DateTime? CancelRequestDate { get; set; }
        public DateTime? RejectRequestDate { get; set; }
        public DateTime? CancelRequestDelayedDate { get; set; }
        public DateTime? RejectRequestDelayedDate { get; set; }
        public DateTime? DeclineCancelRequestDate { get; set; }
        public DateTime? DeclineRejectRequestDate { get; set; }
        public string DocumentStatusReason { get; set; }
        public string CreatedByUserId { get; set; }
        public FreezeStatusDto FreezeStatus { get; set; }
        public string LateSubmissionRequestNumber { get; set; }
    }
}
