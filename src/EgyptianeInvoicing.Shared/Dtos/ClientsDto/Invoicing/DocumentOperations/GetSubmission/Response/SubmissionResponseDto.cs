using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response;

namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetSubmission.Response
{
    public class GetSubmissionResponseDto
    {
        public string uuid { get; set; }
        public int documentCount { get; set; }
        public DateTime dateTimeReceived { get; set; }
        public string overallStatus { get; set; }
        public List<DocumentSummaryDto> documentSummary { get; set; }
        public RecentDocumentMetadataDto documentSummaryMetadata { get; set; }
    }

}
