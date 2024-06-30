using EgyptianeInvoicing.MVC.Base;
using EgyptianeInvoicing.MVC.Clients.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Enums;
using EgyptianeInvoicing.MVC.Constants;
using EgyptianeInvoicing.Shared.Requests;

namespace EgyptianeInvoicing.MVC.Clients
{
    public class InvoiceClient : BaseClient, IInvoiceClient
    {
        public InvoiceClient(IHttpClientFactory httpClientFactory, ILogger<InvoiceClient> logger, IHttpContextAccessor httpContextAccessor)
          : base(httpClientFactory.CreateClient("ApiClient"), logger, httpContextAccessor)
        {
        }

        public async Task<BaseResponse<InvoiceDto>> CreateInvoiceAsync(CreateInvoiceRequestDto request)
        {
            return await PostAsync<CreateInvoiceRequestDto, BaseResponse<InvoiceDto>>("api/v1/invoices/create-invoice", request);
        }

        public async Task<BaseResponse<CustomList<InvoiceDto>>> SearchInvoicesAsync(
            DataTableOptionsDto options,
            InvoiceStatus? invoiceStatus,
            DocumentType? documentType,
            CompanyType? receiverType,
            Guid? receiverId,
            CompanyType? issuerType,
            Guid? issuerId,
            string? eInvoiceId,
            string? internalID)
        {
            var queryString = new QueryStringBuilder()
                .Add("options", options)
                .Add("invoiceStatus", invoiceStatus)
                .Add("documentType", documentType)
                .Add("receiverType", receiverType)
                .Add("receiverId", receiverId)
                .Add("issuerType", issuerType)
                .Add("issuerId", issuerId)
                .Add("eInvoiceId", eInvoiceId)
                .Add("internalID", internalID)
                .ToString();

            return await GetAsync<BaseResponse<CustomList<InvoiceDto>>>($"api/v1/invoices/search{queryString}");
        }
    }
}
