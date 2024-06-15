namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details
{
    public class TaxableItemDto
    {
        public string TaxType { get; set; }
        public decimal Amount { get; set; }
        public string SubType { get; set; }
        public decimal Rate { get; set; }
    }
}
