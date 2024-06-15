namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response
{
    public class RecentDocumentMetadataDto
    {
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool QueryContainsCompleteResultSet { get; set; }
        public int RemainingRecordsCount { get; set; }
    }
}
