namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details
{
    public class InvoiceLineDto
    {
        public string Description { get; set; }
        public string ItemType { get; set; }
        public string ItemCode { get; set; }
        public string UnitType { get; set; }
        public double Quantity { get; set; }
        public string InternalCode { get; set; }
        public double SalesTotal { get; set; }
        public double Total { get; set; }
        public double ValueDifference { get; set; }
        public double TotalTaxableFees { get; set; }
        public double NetTotal { get; set; }
        public double? ItemsDiscount { get; set; }
        public UnitValueDto UnitValue { get; set; }
        public DiscountDto Discount { get; set; }
        public List<TaxableItemDto> TaxableItems { get; set; }
    }
}
