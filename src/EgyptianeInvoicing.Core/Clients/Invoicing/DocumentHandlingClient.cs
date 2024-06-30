using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using EgyptianeInvoicing.Core.Services.Abstractions;
using System.Text;
using System.Text.Json;

namespace EgyptianeInvoicing.Core.Clients.Invoicing
{
    public class DocumentHandlingClient : IDocumentHandlingClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _invoicingClient;
        private readonly ICompanyRepository _companyRepository;

        public DocumentHandlingClient(IHttpClientFactory httpClientFactory, ICompanyRepository companyRepository)
        {
            _httpClientFactory = httpClientFactory;
            _invoicingClient = httpClientFactory.CreateClient("SystemApiBaseUrl");
            _companyRepository = companyRepository;
        }
        public async Task<HttpResponseMessage> DeclineCancelDocumentAsync(Guid companyId, string documentUUID, string declineReason)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = await _companyRepository.GetCompanyTokenByIdAsync(companyId);
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
        public async Task<HttpResponseMessage> DeclineRejectionAsync(Guid companyId, string documentUUID)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = await _companyRepository.GetCompanyTokenByIdAsync(companyId);
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
