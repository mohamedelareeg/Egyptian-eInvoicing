using System.Net;
using System.Text.Json;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocument.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocumentDetails.Response;

namespace EgyptianeInvoicing.Core.Clients.Invoicing
{
    public class DocumentRetrievalClient : IDocumentRetrievalClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _invoicingClient;
        private readonly ICompanyRepository _companyRepository;

        public DocumentRetrievalClient(IHttpClientFactory httpClientFactory, ICompanyRepository companyRepository)
        {
            _httpClientFactory = httpClientFactory;
            _invoicingClient = httpClientFactory.CreateClient("SystemApiBaseUrl");
            _companyRepository = companyRepository;
        }
        public async Task<DocumentRetrievalDto> GetDocumentAsync(Guid companyId, string documentUUID)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = await _companyRepository.GetCompanyTokenByIdAsync(companyId);
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var url = $"api/v1.0/documents/{documentUUID}/raw";

            var response = await _invoicingClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var document = JsonSerializer.Deserialize<DocumentRetrievalDto>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return document;
            }
            else
            {
                throw new Exception($"Failed to retrieve document. Status code: {response.StatusCode}");
            }
        }
        //private string GetCurrentLanguagePreference()
        //{
        //    return System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        //}
        public async Task<byte[]> GetDocumentPdfAsync(Guid companyId, string documentUUID)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = await _companyRepository.GetCompanyTokenByIdAsync(companyId);
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            //var currentLanguage = GetCurrentLanguagePreference() ?? "en"; 

            _invoicingClient.DefaultRequestHeaders.Add("Accept-Language", "ar");
            var url = $"/api/v1.0/documents/{documentUUID}/pdf";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = await _invoicingClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    if (response.Content.Headers.ContentType?.MediaType == "application/pdf")
                    {
                        var pdfBytes = await response.Content.ReadAsByteArrayAsync();
                        return pdfBytes;
                    }
                    else
                    {
                        throw new Exception("Unexpected content type received. Expected application/pdf.");
                    }
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Document not found.");
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new Exception("Document is not ready for download.");
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new Exception("Access to document is forbidden.");
                }
                else
                {
                    throw new Exception($"Failed to retrieve PDF. Status code: {response.StatusCode}");
                }
            }
        }

        public async Task<DocumentDetailsDto> GetDocumentDetailsAsync(Guid companyId, string documentUUID)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = await _companyRepository.GetCompanyTokenByIdAsync(companyId);
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var url = $"api/v1.0/documents/{documentUUID}/details";
            var response = await _invoicingClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var documentDetails = JsonSerializer.Deserialize<DocumentDetailsDto>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return documentDetails;
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve document details. Status code: {response.StatusCode}. Reason: {response.ReasonPhrase}");
            }
        }

    }
}
