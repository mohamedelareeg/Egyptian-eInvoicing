using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using EgyptianeInvoicing.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Shared.Requests
{
    public class CreateInvoiceRequestDto
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

        public CreateInvoiceRequestDto()
        {
            InvoiceLines = new List<InvoiceLineDto>();
        }
    }
}
