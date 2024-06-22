namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response
{
    public class RecentDocumentMetadataDto
    {
        public int? totalPages { get; set; }
        public int? totalCount { get; set; }
        public bool? queryContainsCompleteResultSet { get; set; }
        public int? remainingRecordsCount { get; set; }
        public string? continuationToken { get; set; }
    }
}
