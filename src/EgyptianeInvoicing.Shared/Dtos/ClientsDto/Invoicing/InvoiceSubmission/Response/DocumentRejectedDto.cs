using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocument.Response;

namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Response
{
    public class DocumentRejectedDto
    {
        public string internalId { get; set; }
        public Error error { get; set; }
    }
}
