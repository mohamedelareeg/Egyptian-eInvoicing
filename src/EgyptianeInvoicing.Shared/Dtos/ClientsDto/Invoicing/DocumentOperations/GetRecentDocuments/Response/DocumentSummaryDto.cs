namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response
{
    public class DocumentSummaryDto
    {
        public string? uuid { get; set; }
        public string? submissionUUID { get; set; }
        public string? longId { get; set; }
        public string? publicUrl { get; set; }
        public string? internalId { get; set; }
        public string? typeName { get; set; }
        public string? documentTypeNamePrimaryLang { get; set; }
        public string? documentTypeNameSecondaryLang { get; set; }
        public string? typeVersionName { get; set; }
        public string? issuerId { get; set; }
        public string? issuerName { get; set; }
        public string? issuerType { get; set; }
        public string? receiverId { get; set; }
        public string? receiverName { get; set; }
        public string? receiverType { get; set; }
        public DateTime? dateTimeIssued { get; set; }
        public DateTime? dateTimeReceived { get; set; }
        public decimal totalSales { get; set; }
        public decimal totalDiscount { get; set; }
        public decimal netAmount { get; set; }
        public decimal total { get; set; }
        public string? status { get; set; }
        public DateTime? cancelRequestDate { get; set; }
        public DateTime? rejectRequestDate { get; set; }
        public DateTime? cancelRequestDelayedDate { get; set; }
        public DateTime? rejectRequestDelayedDate { get; set; }
        public DateTime? declineCancelRequestDate { get; set; }
        public DateTime? declineRejectRequestDate { get; set; }
        public string? documentStatusReason { get; set; }
        public string? createdByUserId { get; set; }
        public FreezeStatusDto? freezeStatus { get; set; }
        public string? lateSubmissionRequestNumber { get; set; }
    }
}
