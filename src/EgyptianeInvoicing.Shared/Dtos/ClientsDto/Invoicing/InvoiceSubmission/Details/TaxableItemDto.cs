namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details
{
    public class TaxableItemDto
    {
        public string? TaxType { get; set; }
        public double? Amount { get; set; }
        public string? SubType { get; set; }
        public double? Rate { get; set; }
    }
}
