namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.DocumentsType.Response
{
    public class WorkflowParameterDto
    {
        public int Id { get; set; }
        public string Parameter { get; set; }
        public decimal Value { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
    }


}
