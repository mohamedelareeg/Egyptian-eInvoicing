using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetSubmission.Response;

namespace EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions
{
    public interface IDocumentOperationsClient
    {
        Task<HttpResponseMessage> CancelDocumentAsync(string documentUUID, string reason);
        Task<HttpResponseMessage> RejectDocumentAsync(string documentUUID, string reason);
        Task<RecentDocumentsDto> GetRecentDocumentsAsync(
            DateTime? submissionDateFrom = null,
            DateTime? submissionDateTo = null,
            DateTime? issueDateFrom = null,
            DateTime? issueDateTo = null,
            int pageSize = 10,
            int pageNo = 1,
            string direction = "",
            string status = "",
            string documentType = "",
            string receiverType = "",
            string receiverId = "",
            string issuerType = "",
            string issuerId = ""
        );
        Task<RecentDocumentsDto> SearchDocumentsAsync(
            DateTime? submissionDateFrom = null,
            DateTime? submissionDateTo = null,
            DateTime? issueDateFrom = null,
            DateTime? issueDateTo = null,
            string continuationToken = "",
            int pageSize = 10,
            string direction = "",
            string status = "",
            string documentType = "",
            string receiverType = "",
            string receiverId = "",
            string issuerType = "",
            string issuerId = "",
            string uuid = "",
            string internalID = ""
        );
        Task<SubmissionResponseDto> GetSubmissionAsync(string submissionUUID, string pageSize = "10", string pageNo = "1");

    }
}

