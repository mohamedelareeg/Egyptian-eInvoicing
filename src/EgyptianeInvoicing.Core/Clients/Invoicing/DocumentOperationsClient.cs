using System.Net;
using System.Text;
using System.Text.Json;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetSubmission.Response;

namespace EgyptianeInvoicing.Core.Clients.Invoicing
{
    public class DocumentOperationsClient : IDocumentOperationsClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _invoicingClient;
        private readonly ISecureStorageService _secureStorageService;

        public DocumentOperationsClient(IHttpClientFactory httpClientFactory, ISecureStorageService secureStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _invoicingClient = httpClientFactory.CreateClient("InvoicingBaseUrl");
            _secureStorageService = secureStorageService;
        }
        public async Task<HttpResponseMessage> CancelDocumentAsync(string documentUUID, string reason)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var requestBody = new
            {
                status = "cancelled",
                reason
            };
            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var url = $"api/v1.0/documents/state/{documentUUID}/state";

            var response = await _invoicingClient.PutAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                var statusCode = response.StatusCode;

                var errorMessage = await response.Content.ReadAsStringAsync();

                switch (statusCode)
                {
                    case HttpStatusCode.BadRequest:
                        throw new HttpRequestException($"Bad request: {errorMessage}");
                    case HttpStatusCode.NotFound:
                        throw new HttpRequestException($"Not found: {errorMessage}");
                    default:
                        throw new HttpRequestException($"Unexpected status code: {statusCode}. Error message: {errorMessage}");
                }
            }

            return response;
        }


        public async Task<HttpResponseMessage> RejectDocumentAsync(string documentUUID, string reason)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var requestBody = new
            {
                status = "rejected",
                reason
            };
            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var url = $"api/v1.0/documents/state/{documentUUID}/state";
            var response = await _invoicingClient.PutAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                var statusCode = response.StatusCode;

                var errorMessage = await response.Content.ReadAsStringAsync();

                switch (statusCode)
                {
                    case HttpStatusCode.BadRequest:
                        throw new HttpRequestException($"Bad request: {errorMessage}");
                    case HttpStatusCode.NotFound:
                        throw new HttpRequestException($"Not found: {errorMessage}");
                    default:
                        throw new HttpRequestException($"Unexpected status code: {statusCode}. Error message: {errorMessage}");
                }
            }

            return response;
        }
        public async Task<RecentDocumentsDto> GetRecentDocumentsAsync(
            DateTime submissionDateFrom,
            DateTime submissionDateTo,
            int pageSize = 10,
            int pageNo = 1,
            string issueDateFrom = "",
            string issueDateTo = "",
            string direction = "",
            string status = "Valid",
            string documentType = "i",
            string receiverType = "",
            string receiverId = "",
            string issuerType = "",
            string issuerId = "")
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("PageSize", pageSize.ToString());
            _invoicingClient.DefaultRequestHeaders.Add("PageNo", pageNo.ToString());

            var queryParameters = new List<string>
                {
                    $"pageNo={pageNo}",
                    $"pageSize={pageSize}",
                    $"submissionDateFrom={submissionDateFrom:yyyy-MM-ddTHH:mm:ss}",
                    $"submissionDateTo={submissionDateTo:yyyy-MM-ddTHH:mm:ss}"
                };

            if (!string.IsNullOrEmpty(issueDateFrom)) queryParameters.Add($"issueDateFrom={issueDateFrom}");
            if (!string.IsNullOrEmpty(issueDateTo)) queryParameters.Add($"issueDateTo={issueDateTo}");
            if (!string.IsNullOrEmpty(direction)) queryParameters.Add($"direction={direction}");
            if (!string.IsNullOrEmpty(status)) queryParameters.Add($"status={status}");
            if (!string.IsNullOrEmpty(documentType)) queryParameters.Add($"documentType={documentType}");
            if (!string.IsNullOrEmpty(receiverType)) queryParameters.Add($"receiverType={receiverType}");
            if (!string.IsNullOrEmpty(receiverId)) queryParameters.Add($"receiverId={receiverId}");
            if (!string.IsNullOrEmpty(issuerType)) queryParameters.Add($"issuerType={issuerType}");
            if (!string.IsNullOrEmpty(issuerId)) queryParameters.Add($"issuerId={issuerId}");

            var url = $"api/v1.0/documents/recent?{string.Join("&", queryParameters)}";

            var response = await _invoicingClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<RecentDocumentsDto>(responseContent);
                return apiResponse;
            }

            throw new HttpRequestException($"Request failed with status code {response.StatusCode} and message {response.ReasonPhrase}");
        }

        public async Task<RecentDocumentsDto> SearchDocumentsAsync(DateTime? submissionDateFrom = null, DateTime? submissionDateTo = null, DateTime? issueDateFrom = null, DateTime? issueDateTo = null, string continuationToken = "", int pageSize = 100, string direction = "", string status = "", string documentType = "", string receiverType = "", string receiverId = "", string issuerType = "", string issuerId = "", string uuid = "", string internalID = "")
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("PageSize", pageSize.ToString());

            var queryParameters = new List<string>
                {
                    $"pageSize={pageSize}"
                };

            if (submissionDateFrom.HasValue) queryParameters.Add($"submissionDateFrom={submissionDateFrom:yyyy-MM-ddTHH:mm:ss}");
            if (submissionDateTo.HasValue) queryParameters.Add($"submissionDateTo={submissionDateTo:yyyy-MM-ddTHH:mm:ss}");
            if (issueDateFrom.HasValue) queryParameters.Add($"issueDateFrom={issueDateFrom:yyyy-MM-ddTHH:mm:ss}");
            if (issueDateTo.HasValue) queryParameters.Add($"issueDateTo={issueDateTo:yyyy-MM-ddTHH:mm:ss}");
            if (!string.IsNullOrEmpty(continuationToken)) queryParameters.Add($"continuationToken={continuationToken}");
            if (!string.IsNullOrEmpty(uuid)) queryParameters.Add($"uuid={uuid}");
            if (!string.IsNullOrEmpty(internalID)) queryParameters.Add($"internalID={internalID}");
            if (!string.IsNullOrEmpty(direction)) queryParameters.Add($"direction={direction}");
            if (!string.IsNullOrEmpty(status)) queryParameters.Add($"status={status}");
            if (!string.IsNullOrEmpty(documentType)) queryParameters.Add($"documentType={documentType}");
            if (!string.IsNullOrEmpty(receiverType)) queryParameters.Add($"receiverType={receiverType}");
            if (!string.IsNullOrEmpty(receiverId)) queryParameters.Add($"receiverId={receiverId}");
            if (!string.IsNullOrEmpty(issuerType)) queryParameters.Add($"issuerType={issuerType}");
            if (!string.IsNullOrEmpty(issuerId)) queryParameters.Add($"issuerId={issuerId}");

            var url = $"api/v1.0/documents/search?{string.Join("&", queryParameters)}";

            var response = await _invoicingClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<RecentDocumentsDto>(responseContent);
                return apiResponse;
            }

            throw new HttpRequestException($"Request failed with status code {response.StatusCode} and message {response.ReasonPhrase}");
        }

        public async Task<SubmissionResponseDto> GetSubmissionAsync(string submissionUUID, string pageSize = "10", string pageNo = "1")
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("PageSize", pageSize);
            _invoicingClient.DefaultRequestHeaders.Add("PageNo", pageNo);

            var url = $"/api/v1.0/documentsubmissions/{submissionUUID}?pageNo={pageNo}&pageSize={pageSize}";

            var response = await _invoicingClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var submissionResponse = JsonSerializer.Deserialize<SubmissionResponseDto>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return submissionResponse;
        }
    }
}
