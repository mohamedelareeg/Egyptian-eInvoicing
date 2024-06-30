namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details
{
    public class UnitValueDto
    {
        public string? CurrencySold { get; set; }
        public double? AmountEGP { get; set; }
        public double? AmountSold { get; set; }
        public decimal? CurrencyExchangeRate { get; set; }
    }
}
