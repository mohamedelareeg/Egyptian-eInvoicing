namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.GetPackagesRequest.Response
{

    public class DocumentPackageResponseDto
    {
        public DocumentPackageInformationDto[] Result { get; set; }
        public DocumentPackageMetadataDto[] Metadata { get; set; }
    }
}
