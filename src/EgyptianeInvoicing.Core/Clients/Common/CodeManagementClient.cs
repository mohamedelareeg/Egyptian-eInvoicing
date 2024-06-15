using EgyptianeInvoicing.Shared.Dtos;
using System.Net;
using System.Text;
using System.Text.Json;
using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using EgyptianeInvoicing.Core.Services.Abstractions;
using System.Net.Http.Headers;
using System.Net.Http;
using static MongoDB.Libmongocrypt.CryptContext;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.CreateEGSCode.Request;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.GetCodeDetailsByItemCode.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.RequestCodeReuse.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.SearchCodeUsage.Request;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.SearchPublishedCodes.Request;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.UpdateCode.Request;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.UpdateEGSCodeUsage.Request;

namespace EgyptianeInvoicing.Core.Clients.Common
{
    public class CodeManagementClient : ICodeManagementClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _invoicingClient;
        private readonly ISecureStorageService _secureStorageService;

        public CodeManagementClient(IHttpClientFactory httpClientFactory, ISecureStorageService secureStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _invoicingClient = httpClientFactory.CreateClient("InvoicingBaseUrl");
            _secureStorageService = secureStorageService;
        }

        //POST
        //{{apiBaseUrl}}/api/v1.0/codetypes/requests/codes
        public async Task<HttpResponseMessage> CreateEGSCodeUsageAsync(List<CreateEGSCodeUsageItemDto> request)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");


            var jsonRequest = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _invoicingClient.PostAsync($"api/v1.0/codetypes/requests/codes", content);

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

        //GET
        //{{apiBaseUrl}}/api/v1.0/codetypes/requests/my?Active=true&Status=Approved&PageSize=10&PageNumber=1&OrderDirections=Descending
        public async Task<List<CodeUsageRequestDetailsDto>> SearchCodeUsageRequestsAsync(string active, string status, string pageSize, string pageNumber, string orderDirection)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Accept-Language", "en");

            var url = $"api/v1.0/codetypes/requests/my?Active={active}&Status={status}&PageSize={pageSize}&PageNumber={pageNumber}&OrderDirections={orderDirection}";
            var response = await _invoicingClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var searchCodeUsageRequestsResponse = JsonSerializer.Deserialize<List<CodeUsageRequestDetailsDto>>(responseBody);

            return searchCodeUsageRequestsResponse;
        }

        //PUT
        //{{apiBaseUrl}}/api/v1.0/codetypes/requests/codeusages
        public async Task<HttpResponseMessage> RequestCodeReuseAsync(List<CodeUsageItemDto> request)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _invoicingClient.DefaultRequestHeaders.Add("Accept-Language", "en");

            var jsonRequest = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _invoicingClient.PutAsync($"api/v1.0/codetypes/requests/codeusages", httpContent);
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
        //GET
        //{{apiBaseUrl}}/api/v1.0/codetypes/:codeType/codes?ParentLevelName=GPC Level 4 Code - Brick&OnlyActive=true&ActiveFrom=2019-01-01T00:00:00Z&Ps=10&Pn=1&OrdDir=Descending&CodeTypeLevelNumber=5
        public async Task<List<PublishedCodeDto>> SearchPublishedCodesAsync(string codeType, string parentLevelName, bool onlyActive, DateTime activeFrom, int pageSize, int pageNumber)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Accept-Language", "en");

            // Construct the query parameters
            var queryParameters = new StringBuilder();
            queryParameters.Append($"ParentLevelName={Uri.EscapeDataString(parentLevelName)}");
            queryParameters.Append($"&OnlyActive={onlyActive}");
            queryParameters.Append($"&ActiveFrom={activeFrom.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")}");
            queryParameters.Append($"&Ps={pageSize}");
            queryParameters.Append($"&Pn={pageNumber}");

            // Construct the URL
            var url = $"/api/v1.0/codetypes/{codeType}/codes?{queryParameters}";

            var response = await _invoicingClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var searchPublishedCodesResponse = JsonSerializer.Deserialize<List<PublishedCodeDto>>(responseBody);

            return searchPublishedCodesResponse;
        }

        //GET
        //{{apiBaseUrl}}/api/v1.0/codetypes/:codeType/codes/:itemCode
        public async Task<GetCodeDetailsResponseDto> GetCodeDetailsByItemCodeAsync(string codeType, string itemCode)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var response = await _invoicingClient.GetAsync($"api/v1.0/codetypes/{codeType}/codes/{itemCode}");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var getCodeDetailsResponse = JsonSerializer.Deserialize<GetCodeDetailsResponseDto>(responseBody);

            return getCodeDetailsResponse;
        }

        //PUT
        //{{apiBaseUrl}}/api/v1.0/codetypes/requests/codes/:codeUsageRequestId
        public async Task<HttpResponseMessage> UpdateEGSCodeUsageAsync(int codeUsageRequestId, UpdateEGSCodeUsageRequestDto request)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Accept-Language", "en");

            var jsonRequest = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _invoicingClient.PutAsync($"api/v1.0/codetypes/requests/codes/{codeUsageRequestId}", content);
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

        //PUT
        //{{apiBaseUrl}}/api/v1.0/codetypes/:codeType/codes/:itemCode
        public async Task<HttpResponseMessage> UpdateCodeAsync(string codeType, string itemCode, CodeUpdateRequestDto codeUpdateRequest)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _invoicingClient.DefaultRequestHeaders.Add("Accept-Language", "en");

            var jsonContent = JsonSerializer.Serialize(codeUpdateRequest);
            var requestUri = $"api/v1.0/codetypes/{codeType}/codes/{itemCode}";
            var response = await _invoicingClient.PutAsync(requestUri, new StringContent(jsonContent, Encoding.UTF8, "application/json"));

            // Handle error responses
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

    }
}
