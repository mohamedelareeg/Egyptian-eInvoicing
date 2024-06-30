using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Response;

namespace EgyptianeInvoicing.Core.Clients.Invoicing
{
    public class InvoiceSubmissionClient : IInvoiceSubmissionClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _invoicingClient;
        private readonly ICompanyRepository _companyRepository;
        public InvoiceSubmissionClient(IHttpClientFactory httpClientFactory, ICompanyRepository companyRepository)
        {
            _httpClientFactory = httpClientFactory;
            _invoicingClient = httpClientFactory.CreateClient("SystemApiBaseUrl");
            _companyRepository = companyRepository;
        }
        public async Task<SubmissionResponseDto> SubmitRegularInvoiceAsync(Guid companyId, List<EInvoiceDto> request)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = await _companyRepository.GetCompanyTokenByIdAsync(companyId);
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new HttpRequestException($"Token is null or empty. Retrieved token: '{accessToken}'");
            }
            _invoicingClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            //_invoicingClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            var payload = new
            {
                documents = request.Select(doc => new EInvoiceDto
                {
                    Issuer = new CompanyDto
                    {
                        Address = new AddressDto
                        {
                            BranchID = doc.Issuer?.Address?.BranchID.ToString(),
                            Country = doc.Issuer?.Address?.Country,
                            Governate = doc.Issuer?.Address?.Governate,
                            RegionCity = doc.Issuer?.Address?.RegionCity,
                            Street = doc.Issuer?.Address?.Street,
                            BuildingNumber = doc.Issuer?.Address?.BuildingNumber,
                            PostalCode = doc.Issuer?.Address?.PostalCode,
                            Floor = doc.Issuer?.Address?.Floor,
                            Room = doc.Issuer?.Address?.Room,
                            Landmark = doc.Issuer?.Address?.Landmark,
                            AdditionalInformation = doc.Issuer?.Address?.AdditionalInformation
                        },
                        Type = doc.Issuer?.Type,
                        Id = doc.Issuer?.Id,
                        Name = doc.Issuer?.Name,
                        ReferenceId = doc.Issuer?.ReferenceId
                    },
                    Receiver = new CompanyDto
                    {
                        Address = new AddressDto
                        {
                            BranchID = null,
                            Country = doc.Receiver?.Address?.Country,
                            Governate = doc.Receiver?.Address?.Governate,
                            RegionCity = doc.Receiver?.Address?.RegionCity,
                            Street = doc.Receiver?.Address?.Street,
                            BuildingNumber = doc.Receiver?.Address?.BuildingNumber,
                            PostalCode = doc.Receiver?.Address?.PostalCode,
                            Floor = doc.Receiver?.Address?.Floor,
                            Room = doc.Receiver?.Address?.Room,
                            Landmark = doc.Receiver?.Address?.Landmark,
                            AdditionalInformation = doc.Receiver?.Address?.AdditionalInformation
                        },
                        Type = doc.Receiver?.Type,
                        Id = doc.Receiver?.Id,
                        Name = doc.Receiver?.Name,
                        ReferenceId = doc.Receiver?.ReferenceId
                    },
                    DocumentType = doc.DocumentType,
                    DocumentTypeVersion = doc.DocumentTypeVersion,
                    DateTimeIssued = doc.DateTimeIssued,
                    TaxpayerActivityCode = doc.TaxpayerActivityCode,
                    InternalID = doc.InternalID,
                    PurchaseOrderReference = doc.PurchaseOrderReference,
                    PurchaseOrderDescription = doc.PurchaseOrderDescription,
                    SalesOrderReference = doc.SalesOrderReference,
                    SalesOrderDescription = doc.SalesOrderDescription,
                    ProformaInvoiceNumber = doc.ProformaInvoiceNumber,
                    Payment = doc.Payment,
                    Delivery = doc.Delivery,
                    InvoiceLines = doc.InvoiceLines?.Select(line => new InvoiceLineDto
                    {
                        Description = line.Description,
                        ItemType = line.ItemType,
                        ItemCode = line.ItemCode,
                        UnitType = line.UnitType,
                        Quantity = line.Quantity,
                        InternalCode = line.InternalCode,
                        SalesTotal = line.SalesTotal,
                        Total = line.Total,
                        ValueDifference = line.ValueDifference,
                        TotalTaxableFees = line.TotalTaxableFees,
                        NetTotal = line.NetTotal,
                        ItemsDiscount = line.ItemsDiscount,
                        UnitValue = new UnitValueDto
                        {
                            CurrencySold = line.UnitValue?.CurrencySold,
                            AmountEGP = line.UnitValue?.AmountEGP??0,
                            AmountSold = line.UnitValue?.AmountSold??0,
                            CurrencyExchangeRate = line.UnitValue?.CurrencyExchangeRate??0
                        },
                        Discount = line.Discount,
                        TaxableItems = line.TaxableItems
                    }).Where(line => line != null).ToList(),
                    TotalDiscountAmount = doc.TotalDiscountAmount,
                    TotalSalesAmount = doc.TotalSalesAmount,
                    NetAmount = doc.NetAmount,
                    TaxTotals = doc.TaxTotals,
                    TotalAmount = doc.TotalAmount,
                    ExtraDiscountAmount = doc.ExtraDiscountAmount,
                    TotalItemsDiscountAmount = doc.TotalItemsDiscountAmount,
                    Signatures = doc.Signatures?.Select(sig => new SignatureDto
                    {
                        SignatureType = sig.SignatureType,
                        Value = sig.Value
                    }).Where(sig => sig != null).ToList(),
                    References = doc.References
                }).Where(doc => doc != null).ToList()
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // Ignore properties with null values
                IgnoreReadOnlyProperties = true, // Ignore readonly properties
                NumberHandling = JsonNumberHandling.Strict // Serialize numbers as numbers
            };

            var jsonContent = JsonSerializer.Serialize(payload, jsonOptions);
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
        public async Task<SubmissionResponseDto> SubmitDebitNoteAsync(Guid companyId, List<EInvoiceDto> request)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = await _companyRepository.GetCompanyTokenByIdAsync(companyId);
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
        public async Task<SubmissionResponseDto> SubmitCreditNoteAsync(Guid companyId, List<EInvoiceDto> request)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = await _companyRepository.GetCompanyTokenByIdAsync(companyId);
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
        public async Task<SubmissionResponseDto> SubmitExportInvoiceAsync(Guid companyId, List<EInvoiceDto> request)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = await _companyRepository.GetCompanyTokenByIdAsync(companyId);
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
        public async Task<SubmissionResponseDto> SubmitExportDebitNoteAsync(Guid companyId, List<EInvoiceDto> request)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = await _companyRepository.GetCompanyTokenByIdAsync(companyId);
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
        public async Task<SubmissionResponseDto> SubmitExportCreditNoteAsync(Guid companyId, List<EInvoiceDto> request)
        {
            _invoicingClient.DefaultRequestHeaders.Clear();
            var accessToken = await _companyRepository.GetCompanyTokenByIdAsync(companyId);
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
