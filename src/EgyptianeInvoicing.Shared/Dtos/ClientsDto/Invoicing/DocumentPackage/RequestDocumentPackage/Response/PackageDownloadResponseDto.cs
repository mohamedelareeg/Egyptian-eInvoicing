using System.Text.Json.Serialization;

namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.RequestDocumentPackage.Response
{
    public class PackageDownloadResponseDto
    {
        public bool IsReady { get; set; } = true;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string ContentDisposition { get; set; }
        public string ContentType { get; set; }
        public long? ContentLength { get; set; }
        public byte[] PackageData { get; set; }
    }
}
