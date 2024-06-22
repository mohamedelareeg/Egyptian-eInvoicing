using System.Net;
using System.Text;
using System.Text.Json;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Response;

namespace EgyptianeInvoicing.Core.Clients.Invoicing
{
    public class InvoiceSubmissionClient : IInvoiceSubmissionClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _invoicingClient;
        private readonly ISecureStorageService _secureStorageService;
        public InvoiceSubmissionClient(IHttpClientFactory httpClientFactory, ISecureStorageService secureStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _invoicingClient = httpClientFactory.CreateClient("SystemApiBaseUrl");
            _secureStorageService = secureStorageService;
        }
        public async Task<SubmissionResponseDto> SubmitRegularInvoiceAsync(List<DocumentDto> request)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _invoicingClient.PostAsync($"api/v1.0/documentsubmissions", content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var submissionResponse = JsonSerializer.Deserialize<SubmissionResponseDto>(responseBody);
                return submissionResponse;
            }
            else
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
        }
        public async Task<SubmissionResponseDto> SubmitDebitNoteAsync(List<DocumentDto> request)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _invoicingClient.PostAsync($"api/v1.0/documentsubmissions", content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var submissionResponse = JsonSerializer.Deserialize<SubmissionResponseDto>(responseBody);
                return submissionResponse;
            }
            else
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
        }
        public async Task<SubmissionResponseDto> SubmitCreditNoteAsync(List<DocumentDto> request)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _invoicingClient.PostAsync($"api/v1.0/documentsubmissions", content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var submissionResponse = JsonSerializer.Deserialize<SubmissionResponseDto>(responseBody);
                return submissionResponse;
            }
            else
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
        }
        public async Task<SubmissionResponseDto> SubmitExportInvoiceAsync(List<DocumentDto> request)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _invoicingClient.PostAsync($"api/v1.0/documentsubmissions", content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var submissionResponse = JsonSerializer.Deserialize<SubmissionResponseDto>(responseBody);
                return submissionResponse;
            }
            else
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
        }
        public async Task<SubmissionResponseDto> SubmitExportDebitNoteAsync(List<DocumentDto> request)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _invoicingClient.PostAsync($"api/v1.0/documentsubmissions", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var submissionResponse = JsonSerializer.Deserialize<SubmissionResponseDto>(responseBody);
                return submissionResponse;
            }
            else
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
        }
        public async Task<SubmissionResponseDto> SubmitExportCreditNoteAsync(List<DocumentDto> request)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _invoicingClient.PostAsync($"api/v1.0/documentsubmissions", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var submissionResponse = JsonSerializer.Deserialize<SubmissionResponseDto>(responseBody);
                return submissionResponse;
            }
            else
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
        }

    }
}
