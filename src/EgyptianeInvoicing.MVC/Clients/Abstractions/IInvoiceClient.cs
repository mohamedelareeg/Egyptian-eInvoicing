using EgyptianeInvoicing.MVC.Base;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Enums;
using EgyptianeInvoicing.Shared.Requests;

namespace EgyptianeInvoicing.MVC.Clients.Abstractions
{
    public interface IInvoiceClient
    {
        Task<BaseResponse<InvoiceDto>> CreateInvoiceAsync(CreateInvoiceRequestDto request);
        Task<BaseResponse<CustomList<InvoiceDto>>> SearchInvoicesAsync(
            DataTableOptionsDto options,
            InvoiceStatus? invoiceStatus,
            DocumentType? documentType,
            CompanyType? receiverType,
            Guid? receiverId,
            CompanyType? issuerType,
            Guid? issuerId,
            string? eInvoiceId,
            string? internalID);
    }
}
