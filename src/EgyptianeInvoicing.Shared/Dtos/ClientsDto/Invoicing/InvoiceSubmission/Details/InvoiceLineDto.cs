namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details
{
    public class InvoiceLineDto
    {
        public string Description { get; set; }
        public string ItemType { get; set; }
        public string ItemCode { get; set; }
        public string UnitType { get; set; }
        public int Quantity { get; set; }
        public string InternalCode { get; set; }
        public decimal SalesTotal { get; set; }
        public decimal Total { get; set; }
        public decimal ValueDifference { get; set; }
        public decimal TotalTaxableFees { get; set; }
        public decimal NetTotal { get; set; }
        public decimal ItemsDiscount { get; set; }
        public UnitValueDto UnitValue { get; set; }
        public DiscountDto Discount { get; set; }
        public List<TaxableItemDto> TaxableItems { get; set; }
    }
}
