namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.RequestDocumentPackage.Request
{
    public class DocumentPackageRequestDto
    {
        public string Type { get; set; }
        public string Format { get; set; }
        public bool TruncateIfExceeded { get; set; }
        public DocumentPackageQueryParametersDto QueryParameters { get; set; }
    }
}
