namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.RequestDocumentPackage.Request
{
    public class DocumentPackageQueryParametersDto
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<string> Statuses { get; set; }
        public List<string> ProductsInternalCodes { get; set; }
        public string ReceiverSenderType { get; set; }
        public List<string> DocumentTypeNames { get; set; }
        public string RepresentedTaxpayerFilterType { get; set; }
        public string RepresenteeRin { get; set; }
        public string BranchNumber { get; set; }
        public List<DocumentPackageItemCodeDto> ItemCodes { get; set; }
    }
}
