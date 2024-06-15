namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.DocumentsType.Response
{
    public class DocumentTypeVersionDto
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal VersionNumber { get; set; }
        public string Status { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
        public string JsonSchema { get; set; }
        public string XmlSchema { get; set; }
    }


}
