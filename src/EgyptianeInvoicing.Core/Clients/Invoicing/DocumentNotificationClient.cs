using EgyptianeInvoicing.Shared.Dtos;
using System.Text;
using System.Text.Json;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentNotification.Request;

namespace EgyptianeInvoicing.Core.Clients.Invoicing
{
    public class DocumentNotificationClient : IDocumentNotificationClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _invoicingClient;
        private readonly ISecureStorageService _secureStorageService;
        public DocumentNotificationClient(IHttpClientFactory httpClientFactory, ISecureStorageService secureStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _invoicingClient = httpClientFactory.CreateClient("SystemApiBaseUrl");
            _secureStorageService = secureStorageService;
        }
        public async Task<HttpResponseMessage> ReceiveDocumentNotificationsAsync(string deliveryId, string type, int count, DocumentNotificationMessageDto[] messages)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"ApiKey {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Accept-Language", "en");
            _invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            var requestBody = new
            {
                deliveryId,
                type,
                count,
                message = messages
            };

            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var url = "api/v1.0/notifications/documents";

            var response = await _invoicingClient.PutAsync(url, content);

            return response;
        }
        public async Task<HttpResponseMessage> ReceiveDocumentPackageNotificationAsync(string deliveryId, string packageId)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"ApiKey {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Accept-Language", "en");
            _invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var requestBody = new
            {
                deliveryId,
                type = "document-package-ready",
                count = 1,
                message = new[]
                {
                    new DocumentPackageReadyMessageDto
                    {
                        type = "document-package-ready",
                        packageId = packageId
                    }
                }
            };

            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var url = "api/v1.0/notifications/documentpackages";
            var response = await _invoicingClient.PutAsync(url, content);

            return response;
        }
    }
}
