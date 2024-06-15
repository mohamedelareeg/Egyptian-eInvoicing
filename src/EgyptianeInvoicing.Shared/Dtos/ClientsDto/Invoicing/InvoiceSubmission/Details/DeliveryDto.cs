namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details
{
    public class DeliveryDto
    {
        public string Approach { get; set; }
        public string Packaging { get; set; }
        public DateTime DateValidity { get; set; }
        public string ExportPort { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal NetWeight { get; set; }
        public string Terms { get; set; }
    }
}
