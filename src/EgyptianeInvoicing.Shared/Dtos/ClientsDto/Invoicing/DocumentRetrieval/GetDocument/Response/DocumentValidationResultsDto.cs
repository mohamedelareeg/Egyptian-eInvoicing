namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocument.Response
{
    public class DocumentValidationResultsDto
    {
        public string status { get; set; }
        public List<DocumentValidationStepResultDto> validationSteps { get; set; }
    }
}
