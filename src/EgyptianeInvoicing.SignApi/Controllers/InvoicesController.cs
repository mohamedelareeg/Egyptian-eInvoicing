using EgyptianeInvoicing.Core.Features.Companies.Queries.SearchCompanies;
using EgyptianeInvoicing.Core.Features.Invoices.Commands.CreateInvoice;
using EgyptianeInvoicing.Core.Features.Invoices.Queries.SearchInvoices;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Enums;
using EgyptianeInvoicing.Shared.Requests;
using EgyptianeInvoicing.SignApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EgyptianeInvoicing.SignApi.Controllers
{
    [ApiController]
    [Route("api/v1/invoices")]
    public class InvoicesController : AppControllerBase
    {
        public InvoicesController(ISender sender)
            : base(sender)
        {
        }

        [HttpPost("create-invoice")]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceRequestDto request)
        {
            var command = new CreateInvoiceCommand(
               issuerId: request.IssuerId,
               receiverId: request.ReceiverId,
               einvoiceId: request.EinvoiceId,
               purchaseOrderId: request.PurchaseOrderId,
               salesOrderId: request.SalesOrderId,
               invoiceNumber: request.InvoiceNumber,
               invoiceLines: request.InvoiceLines,
               payment: request.Payment,
               delivery: request.Delivery,
               status: request.Status,
               documentType: request.DocumentType,
               currency: request.Currency,
               extraDiscountAmount: request.ExtraDiscountAmount
           );
            var result = await Sender.Send(command);
            return CustomResult(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchInvoices(
          [FromQuery] DataTableOptionsDto options,
          [FromQuery] InvoiceStatus? invoiceStatus,
          [FromQuery] DocumentType? documentType,
          [FromQuery] CompanyType? receiverType,
          [FromQuery] Guid? receiverId,
          [FromQuery] CompanyType? issuerType,
          [FromQuery] Guid? issuerId,
          [FromQuery] string? eInvoiceId,
          [FromQuery] string? internalID)
        {
            var query = new SearchInvoicesQuery
            {
                Options = options,
                InvoiceStatus = invoiceStatus,
                DocumentType = documentType,
                ReceiverType = receiverType,
                ReceiverId = receiverId,
                IssuerType = issuerType,
                IssuerId = issuerId,
                EInvoiceId = eInvoiceId,
                InternalID = internalID
            };

            var result = await Sender.Send(query);
            return CustomResult(result);
        }
    }
}
