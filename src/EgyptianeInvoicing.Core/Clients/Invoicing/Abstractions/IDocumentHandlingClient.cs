namespace EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions
{
    public interface IDocumentHandlingClient
    {
        Task<HttpResponseMessage> DeclineCancelDocumentAsync(Guid companyId, string documentUUID, string declineReason);
        Task<HttpResponseMessage> DeclineRejectionAsync(Guid companyId, string documentUUID);
    }
}
