namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response
{
    public class RecentDocumentsDto
    {
        public List<DocumentSummaryDto> Result { get; set; }
        public RecentDocumentMetadataDto Metadata { get; set; }
    }
}
