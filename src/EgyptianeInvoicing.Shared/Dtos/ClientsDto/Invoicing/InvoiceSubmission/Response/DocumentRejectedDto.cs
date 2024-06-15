using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocument.Response;

namespace EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Response
{
    public class DocumentRejectedDto
    {
        public string InternalId { get; set; }
        public Error Error { get; set; }
    }
}
