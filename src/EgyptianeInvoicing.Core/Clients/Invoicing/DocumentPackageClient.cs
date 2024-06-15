using System.Net;
using System.Text;
using System.Text.Json;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.GetPackagesRequest.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.RequestDocumentPackage.Request;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.RequestDocumentPackage.Response;

namespace EgyptianeInvoicing.Core.Clients.Invoicing
{
    public class DocumentPackageClient : IDocumentPackageClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _invoicingClient;
        private readonly ISecureStorageService _secureStorageService;
        public DocumentPackageClient(IHttpClientFactory httpClientFactory, ISecureStorageService secureStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _invoicingClient = httpClientFactory.CreateClient("InvoicingBaseUrl");
            _secureStorageService = secureStorageService;
        }
        public async Task<PackageDownloadResponseDto> RequestDocumentPackageAsync(DocumentPackageRequestDto requestDto)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            var jsonContent = new StringContent(JsonSerializer.Serialize(requestDto), Encoding.UTF8, "application/json");

            var response = await _invoicingClient.PostAsync("/api/v1.0/documentpackages/requests", jsonContent);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var contentDisposition = response.Content.Headers.ContentDisposition?.ToString();
                var contentType = response.Content.Headers.ContentType?.ToString();
                var contentLength = response.Content.Headers.ContentLength;

                var packageData = await response.Content.ReadAsByteArrayAsync();

                var downloadResponse = new PackageDownloadResponseDto
                {
                    ContentDisposition = contentDisposition,
                    ContentType = contentType,
                    ContentLength = contentLength,
                    PackageData = packageData
                };

                return downloadResponse;
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return new PackageDownloadResponseDto
                {
                    IsReady = false
                };
            }
            else
            {
                var errorResponseContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorResponseContent);
                throw new HttpRequestException($"Request failed with status code {response.StatusCode} and message {response.ReasonPhrase}");
            }
        }
        public async Task<DocumentPackageResponseDto> GetPackagesRequestAsync(int pageSize, int pageNo, DateTime dateFrom, DateTime dateTo, string documentTypeName = "", string statuses = "", string productsInternalCodes = "", int receiverSenderType = 0, string receiverSenderId = "", string branchNumber = "", string itemCodes = "")
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var queryParams = new StringBuilder();
            queryParams.Append($"dateFrom={dateFrom.ToString("yyyy-MM-ddTHH:mm:ssZ")}&");
            queryParams.Append($"dateTo={dateTo.ToString("yyyy-MM-ddTHH:mm:ssZ")}&");
            if (!string.IsNullOrEmpty(documentTypeName))
                queryParams.Append($"documentTypeName={documentTypeName}&");
            if (!string.IsNullOrEmpty(statuses))
                queryParams.Append($"statuses={statuses}&");
            if (!string.IsNullOrEmpty(productsInternalCodes))
                queryParams.Append($"productsInternalCodes={productsInternalCodes}&");
            queryParams.Append($"receiverSenderType={receiverSenderType}&");
            if (!string.IsNullOrEmpty(receiverSenderId))
                queryParams.Append($"receiverSenderId={receiverSenderId}&");
            if (!string.IsNullOrEmpty(branchNumber))
                queryParams.Append($"branchNumber={branchNumber}&");
            if (!string.IsNullOrEmpty(itemCodes))
                queryParams.Append($"itemCodes={itemCodes}");

            if (queryParams.Length > 0 && queryParams[queryParams.Length - 1] == '&')
                queryParams.Length--;

            var url = $"api/v1.0/documentpackages/requests?pageSize={pageSize}&pageNo={pageNo}&{queryParams.ToString()}";

            var response = await _invoicingClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var packageResponse = JsonSerializer.Deserialize<DocumentPackageResponseDto>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return packageResponse;
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve document package requests. Status code: {response.StatusCode}");
            }
        }
        public async Task<byte[]> GetDocumentPackageAsync(string rid)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = _secureStorageService.GetToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var url = $"api/v1.0/documentpackages/{rid}";

            using (var response = await _invoicingClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        if (stream == null)
                        {
                            throw new Exception("Empty response stream received.");
                        }

                        using (MemoryStream ms = new MemoryStream())
                        {
                            await stream.CopyToAsync(ms);
                            return ms.ToArray();
                        }
                    }
                }
                else
                {
                    throw new Exception($"Failed to download document package. Status code: {response.StatusCode}");
                }
            }
        }

    }
}
