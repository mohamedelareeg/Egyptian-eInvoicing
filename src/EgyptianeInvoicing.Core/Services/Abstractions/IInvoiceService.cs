using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Features.Invoices.Commands.CreateInvoice;
using EgyptianeInvoicing.Core.Models;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using EgyptianeInvoicing.Shared.Enums;
using EgyptianeInvoicing.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Services.Abstractions
{
    public interface IInvoiceService
    {
        Task<Result<Invoice>> CreateInvoiceAsync(
            Guid issuerId,
            Guid receiverId,
            string einvoiceId,
            Guid? purchaseOrderId,
            Guid? salesOrderId,
            string invoiceNumber,
            List<InvoiceLineDto> invoiceLines,
            PaymentDto payment,
            DeliveryDto delivery,
            InvoiceStatus status,
            DocumentType documentType,
            Shared.Enums.Currency currency,
            double extraDiscountAmount,
            CompanyDto receiver = null
        );
    }
}
