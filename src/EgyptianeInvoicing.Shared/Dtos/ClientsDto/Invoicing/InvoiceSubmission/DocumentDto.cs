using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;

namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission
{
    public class DocumentDto
    {
        public CompanyDto Issuer { get; set; }
        public CompanyDto Receiver { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeVersion { get; set; }
        public DateTime DateTimeIssued { get; set; }
        public string TaxpayerActivityCode { get; set; }
        public string InternalId { get; set; }
        public string PurchaseOrderReference { get; set; }
        public string PurchaseOrderDescription { get; set; }
        public string SalesOrderReference { get; set; }
        public string SalesOrderDescription { get; set; }
        public string ProformaInvoiceNumber { get; set; }
        public PaymentDto Payment { get; set; }
        public DeliveryDto Delivery { get; set; }
        public List<InvoiceLineDto> InvoiceLines { get; set; }
        public double TotalDiscountAmount { get; set; }
        public double TotalSalesAmount { get; set; }
        public double NetAmount { get; set; }
        public List<TaxTotalDto> TaxTotals { get; set; }
        public double TotalAmount { get; set; }
        public double ExtraDiscountAmount { get; set; }
        public double TotalItemsDiscountAmount { get; set; }
        public List<SignatureDto> Signatures { get; set; }
        public string[] References { get; set; }
    }
}
