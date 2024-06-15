using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Core.Services.Abstractions;
using System.Text;
using System.Text.Json;

namespace EgyptianeInvoicing.Core.Clients.Invoicing
{
    public class DocumentHandlingClient : IDocumentHandlingClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _invoicingClient;
        private readonly ISecureStorageService _secureStorageService;

        public DocumentHandlingClient(IHttpClientFactory httpClientFactory, ISecureStorageService secureStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _invoicingClient = httpClientFactory.CreateClient("InvoicingBaseUrl");
            _secureStorageService = secureStorageService;
        }
        public async Task<HttpResponseMessage> DeclineCancelDocumentAsync(string documentUUID, string declineReason)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var url = $"api/v1.0/documents/state/{documentUUID}/decline/cancelation";

            var requestBody = new
            {
                status = "declined",
                reason = declineReason
            };

            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _invoicingClient.PutAsync(url, content);

            return response;
        }
        public async Task<HttpResponseMessage> DeclineRejectionAsync(string documentUUID)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var url = $"api/v1.0/documents/state/{documentUUID}/decline/rejection";

            var response = await _invoicingClient.PutAsync(url, null);

            return response;
        }
    }
}
