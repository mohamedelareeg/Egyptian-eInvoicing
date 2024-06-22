namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response
{
    public class RecentDocumentsDto
    {
        public List<DocumentSummaryDto>? result { get; set; }
        public RecentDocumentMetadataDto? metadata { get; set; }
    }
}
