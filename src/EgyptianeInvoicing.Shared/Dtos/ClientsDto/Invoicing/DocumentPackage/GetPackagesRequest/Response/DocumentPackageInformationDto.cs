using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.RequestDocumentPackage.Request;
using System.Text.Json;

namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.GetPackagesRequest.Response
{
    public class DocumentPackageInformationDto
    {
        public string PackageId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public int Format { get; set; }
        public int RequestorTypeId { get; set; }
        public string RequestorTaxpayerRIN { get; set; }
        public string RequestorTaxpayerName { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsExpired { get; set; }
        public DocumentPackageQueryParametersDto QueryParameters { get; set; }
    }
}
