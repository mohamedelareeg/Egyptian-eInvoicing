namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.DocumentsType.Response
{
    public class DocumentTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
        public List<DocumentTypeVersionDto> DocumentTypeVersions { get; set; }
        public List<WorkflowParameterDto> WorkflowParameters { get; set; }
    }


}
