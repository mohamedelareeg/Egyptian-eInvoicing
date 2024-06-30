using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Constants;
using EgyptianeInvoicing.Core.Models;
using EgyptianeInvoicing.Core.ValueObjects;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using EgyptianeInvoicing.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommand : ICommand<InvoiceDto>
    {
        public Guid IssuerId { get; set; }
        public Guid ReceiverId { get; set; }
        public string EinvoiceId { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public Guid? SalesOrderId { get; set; }
        public string InvoiceNumber { get; set; }
        public List<InvoiceLineDto> InvoiceLines { get; set; }
        public PaymentDto Payment { get; set; }
        public DeliveryDto Delivery { get; set; }
        public InvoiceStatus Status { get; set; }
        public DocumentType DocumentType { get; set; }
        public Shared.Enums.Currency Currency { get; set; }
        public double ExtraDiscountAmount { get; set; }
        public CompanyDto Receiver { get; set; }

        public CreateInvoiceCommand(
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
            CompanyDto receiver = null) 
        {
            IssuerId = issuerId;
            ReceiverId = receiverId;
            EinvoiceId = einvoiceId;
            PurchaseOrderId = purchaseOrderId;
            SalesOrderId = salesOrderId;
            InvoiceNumber = invoiceNumber;
            InvoiceLines = invoiceLines;
            Payment = payment;
            Delivery = delivery;
            Status = status;
            DocumentType = documentType;
            Currency = currency;
            ExtraDiscountAmount = extraDiscountAmount;
            Receiver = receiver;
        }
    }
}
