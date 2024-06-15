namespace EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions
{
    public interface IDocumentHandlingClient
    {
        Task<HttpResponseMessage> DeclineCancelDocumentAsync(string documentUUID, string declineReason);
        Task<HttpResponseMessage> DeclineRejectionAsync(string documentUUID);
    }
}
