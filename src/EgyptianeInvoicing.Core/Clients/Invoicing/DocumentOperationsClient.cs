using System;
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
            _invoicingClient = httpClientFactory.CreateClient("SystemApiBaseUrl");
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
            DateTime? submissionDateFrom,
    DateTime? submissionDateTo,
    DateTime? issueDateFrom,
    DateTime? issueDateTo,
    int pageSize = 10,
      int pageNo = 1,
    string direction = "",
    string status = "Valid",
    string documentType = "",
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

            var queryParameters = new List<string>();

            //AppendQueryParam(queryParameters, "submissionDateFrom", "2024-05-01T15:00:59");//submissionDateFrom?.ToString("yyyy-MM-ddTHH:mm:ss")
            //AppendQueryParam(queryParameters, "submissionDateTo", "2024-05-31T20:00:00");//submissionDateTo?.ToString("yyyy-MM-ddTHH:mm:ss"));
            AppendQueryParam(queryParameters, "pageNo", pageNo.ToString());
            AppendQueryParam(queryParameters, "pageSize", pageSize.ToString());
            if (!submissionDateFrom.HasValue)
            {
                submissionDateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            }
            AppendQueryParam(queryParameters, "submissionDateFrom", submissionDateFrom?.ToString("yyyy-MM-ddTHH:mm:ss"));

            if (!submissionDateTo.HasValue)
            {
                submissionDateTo = DateTime.Today.Date.AddDays(1).AddTicks(-1);
            }
            AppendQueryParam(queryParameters, "submissionDateTo", submissionDateTo?.ToString("yyyy-MM-ddTHH:mm:ss"));

            AppendQueryParam(queryParameters, "issueDateFrom", issueDateFrom?.ToString("yyyy-MM-ddTHH:mm:ss"));
            AppendQueryParam(queryParameters, "issueDateTo", issueDateTo?.ToString("yyyy-MM-ddTHH:mm:ss"));
            if (!string.IsNullOrEmpty(receiverType))
            {
                direction = "Sent";
            }
            else if (!string.IsNullOrEmpty(issuerType))
            {
                direction = "Received";
            }
            AppendQueryParam(queryParameters, "direction", direction);
            AppendQueryParam(queryParameters, "status", status);
            AppendQueryParam(queryParameters, "documentType", documentType);
            AppendQueryParam(queryParameters, "receiverType", receiverType);
            AppendQueryParam(queryParameters, "receiverId", receiverId);
            AppendQueryParam(queryParameters, "issuerType", issuerType);
            AppendQueryParam(queryParameters, "issuerId", issuerId);


            var queryString = string.Join("&", queryParameters);
            var url = $"api/v1.0/documents/recent?{queryString}";

            var response = await _invoicingClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<RecentDocumentsDto>(responseContent);
                return apiResponse;
            }

            throw new HttpRequestException($"Request failed with status code {response.StatusCode} and message {response.ReasonPhrase}");
        }

        public async Task<RecentDocumentsDto> SearchDocumentsAsync(
    DateTime? submissionDateFrom,
    DateTime? submissionDateTo,
    DateTime? issueDateFrom,
    DateTime? issueDateTo,
    string continuationToken,
    int pageSize = 10,
    string direction = "",
    string status = "Valid",
    string documentType = "",
    string receiverType = "",
    string receiverId = "",
    string issuerType = "",
    string issuerId = "",
    string uuid = "",
    string internalID = "")
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("PageSize", pageSize.ToString());

            var queryParameters = new List<string>();

            if (!submissionDateFrom.HasValue)
            {
                submissionDateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            }
            AppendQueryParam(queryParameters, "submissionDateFrom", submissionDateFrom?.ToString("yyyy-MM-ddTHH:mm:ss"));

            if (!submissionDateTo.HasValue)
            {
                submissionDateTo = DateTime.Today.Date.AddDays(1).AddTicks(-1);
            }
            AppendQueryParam(queryParameters, "submissionDateTo", submissionDateTo?.ToString("yyyy-MM-ddTHH:mm:ss"));

            //AppendQueryParam(queryParameters, "submissionDateFrom", "2024-05-01T15:00:59");//submissionDateFrom?.ToString("yyyy-MM-ddTHH:mm:ss")
            //AppendQueryParam(queryParameters, "submissionDateTo", "2024-05-31T20:00:00");//submissionDateTo?.ToString("yyyy-MM-ddTHH:mm:ss"));
            AppendQueryParam(queryParameters, "continuationToken", continuationToken);
            AppendQueryParam(queryParameters, "pageSize", pageSize.ToString());
            AppendQueryParam(queryParameters, "issueDateFrom", issueDateFrom?.ToString("yyyy-MM-ddTHH:mm:ss"));
            AppendQueryParam(queryParameters, "issueDateTo", issueDateTo?.ToString("yyyy-MM-ddTHH:mm:ss"));
            if (!string.IsNullOrEmpty(receiverType))
            {
                direction = "Sent";
            }
            else if(!string.IsNullOrEmpty(issuerType))
            {
                direction = "Received";
            }
            AppendQueryParam(queryParameters, "direction", direction);
            AppendQueryParam(queryParameters, "status", status);
            AppendQueryParam(queryParameters, "documentType", documentType);
            AppendQueryParam(queryParameters, "receiverType", receiverType);
            AppendQueryParam(queryParameters, "receiverId", receiverId);
            AppendQueryParam(queryParameters, "issuerType", issuerType);
            AppendQueryParam(queryParameters, "issuerId", issuerId);
            AppendQueryParam(queryParameters, "uuid", uuid);
            AppendQueryParam(queryParameters, "internalID", internalID);

            var queryString = string.Join("&", queryParameters);
            //var url = $"https://api.invoicing.eta.gov.eg/api/v1.0/documents/search?{queryString}";
            var url = $"api/v1.0/documents/search?{queryString}";
            var response = await _invoicingClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<RecentDocumentsDto>(responseContent);
                return apiResponse;
            }

            throw new HttpRequestException($"Request failed with status code {response.StatusCode} and message {response.ReasonPhrase}");
        }

        private void AppendQueryParam(List<string> queryParameters, string paramName, string paramValue)
        {
            if (!string.IsNullOrEmpty(paramValue))
            {
                queryParameters.Add($"{paramName}={Uri.EscapeDataString(paramValue)}");
            }
            else
            {
                queryParameters.Add($"{paramName}=");
            }
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
