using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentNotification.Request;

namespace EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions
{
    public interface IDocumentNotificationClient
    {
        Task<HttpResponseMessage> ReceiveDocumentNotificationsAsync(
          Guid companyId,
          string deliveryId,
          string type,
          int count,
          DocumentNotificationMessageDto[] messages
      );
        Task<HttpResponseMessage> ReceiveDocumentPackageNotificationAsync(Guid companyId, string deliveryId, string packageId);
    }
}
