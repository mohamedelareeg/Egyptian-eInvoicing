using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Shared.Dtos
{
    public class InvoiceDto
    {
        public CompanyDto? Issuer { get; set; }
        public CompanyDto? Receiver { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeVersion { get; set; }
        public string DateTimeIssued { get; set; }
        public string InternalID { get; set; }
        public string PurchaseOrderReference { get; set; }
        public string PurchaseOrderDescription { get; set; }
        public string SalesOrderReference { get; set; }
        public string SalesOrderDescription { get; set; }
        public string ProformaInvoiceNumber { get; set; }

        public PaymentDto Payment { get; set; }
        public DeliveryDto Delivery { get; set; }
        public List<TaxTotalDto> TaxTotals { get; set; }
        public decimal TotalSalesAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TotalItemsDiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
