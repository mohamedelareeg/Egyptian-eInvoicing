using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.DocumentsType.Response;
using System.Text.Json;

namespace EgyptianeInvoicing.Core.Clients.Common
{
    public class DocumentTypesClient : IDocumentTypesClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _invoicingClient;
        private readonly ICompanyRepository _companyRepository;
        public DocumentTypesClient(IHttpClientFactory httpClientFactory, ICompanyRepository companyRepository)
        {
            _httpClientFactory = httpClientFactory;
            _invoicingClient = httpClientFactory.CreateClient("SystemApiBaseUrl");
            _companyRepository = companyRepository;
        }
        //GET
        //{{apiBaseUrl}}/api/v1/documenttypes
        public async Task<List<DocumentTypeDto>> GetDocumentTypesAsync(Guid companyId)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = await _companyRepository.GetCompanyTokenByIdAsync(companyId);
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Accept-Language", "ar");

            var response = await _invoicingClient.GetAsync($"api/v1/documenttypes");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            List<DocumentTypeDto> documentTypes = JsonSerializer.Deserialize<List<DocumentTypeDto>>(responseBody);

            return documentTypes;
        }
        //GET
        //{{apiBaseUrl}}/api/v1/documenttypes/:documentTypeID
        public async Task<DocumentTypeDto> GetDocumentTypeAsync(Guid companyId, int documentTypeId)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = await _companyRepository.GetCompanyTokenByIdAsync(companyId);
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var response = await _invoicingClient.GetAsync($"api/v1/documenttypes/{documentTypeId}");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var documentTypeResponse = JsonSerializer.Deserialize<DocumentTypeDto>(responseBody);

            return documentTypeResponse;
        }
        //GET
        //{{apiBaseUrl}}/api/v1/documenttypes/:documentTypeID/versions/:documentTypeVersionID
        public async Task<DocumentTypeVersionDto> GetDocumentTypeVersionAsync(Guid companyId, int documentTypeId, int documentTypeVersionId)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = await _companyRepository.GetCompanyTokenByIdAsync(companyId);
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _invoicingClient.DefaultRequestHeaders.Add("Accept-Language", "ar");

            var response = await _invoicingClient.GetAsync($"api/v1/documenttypes/{documentTypeId}/versions/{documentTypeVersionId}");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var documentTypeVersionResponse = JsonSerializer.Deserialize<DocumentTypeVersionDto>(responseBody);

            return documentTypeVersionResponse;
        }
    }
}
