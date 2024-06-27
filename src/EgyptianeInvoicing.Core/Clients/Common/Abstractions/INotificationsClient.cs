using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Notifications.Response;

namespace EgyptianeInvoicing.Core.Clients.Common.Abstractions
{
    public interface INotificationsClient
    {
        Task<NotificationsResponseDto> GetNotificationsAsync(Guid companyId, int pageSize, int pageNo, DateTime? dateFrom, DateTime? dateTo, string type, string language, string status, string channel);

    }
}
