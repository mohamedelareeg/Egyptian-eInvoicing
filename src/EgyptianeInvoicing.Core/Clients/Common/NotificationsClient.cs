using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Notifications.Response;
using System.Text.Json;

namespace EgyptianeInvoicing.Core.Clients.Common
{
    public class NotificationsClient : INotificationsClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _invoicingClient;
        private readonly ICompanyRepository _companyRepository;

        public NotificationsClient(IHttpClientFactory httpClientFactory, ICompanyRepository companyRepository)
        {
            _httpClientFactory = httpClientFactory;
            _invoicingClient = httpClientFactory.CreateClient("SystemApiBaseUrl");
            _companyRepository = companyRepository;
        }
        //GET
        //{{apiBaseUrl}}/api/v1/notifications/taxpayer?pageSize=10&pageNo=1&dateFrom=&dateTo=&type=&language=en&status=&channel=
        public async Task<NotificationsResponseDto> GetNotificationsAsync(Guid companyId, int pageSize, int pageNo, DateTime? dateFrom, DateTime? dateTo, string type, string language, string status, string channel)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = await _companyRepository.GetCompanyTokenByIdAsync(companyId);
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var queryString = $"pageSize={pageSize}&pageNo={pageNo}";

            if (dateFrom.HasValue)
                queryString += $"&dateFrom={dateFrom.Value.ToString("yyyy-MM-ddTHH:mm:ss")}";
            if (dateTo.HasValue)
                queryString += $"&dateTo={dateTo.Value.ToString("yyyy-MM-ddTHH:mm:ss")}";
            if (!string.IsNullOrEmpty(type))
                queryString += $"&type={type}";
            if (!string.IsNullOrEmpty(language))
                queryString += $"&language={language}";
            if (!string.IsNullOrEmpty(status))
                queryString += $"&status={status}";
            if (!string.IsNullOrEmpty(channel))
                queryString += $"&channel={channel}";

            var response = await _invoicingClient.GetAsync($"api/v1.0/notifications/taxpayer?{queryString}");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var notificationsResponse = JsonSerializer.Deserialize<NotificationsResponseDto>(responseBody);

            return notificationsResponse;
        }
    }
}
