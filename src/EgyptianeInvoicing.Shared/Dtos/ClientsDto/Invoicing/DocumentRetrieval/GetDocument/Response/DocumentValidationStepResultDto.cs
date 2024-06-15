namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocument.Response
{
    public class DocumentValidationStepResultDto
    {
        public string name { get; set; }
        public string status { get; set; }
        public Error error { get; set; }
    }
}
