using BuildingBlocks.Extentions;
using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Notifications.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Notifications.Queries.GetNotifications
{
    public class GetNotificationsQueryHandler : IQueryHandler<GetNotificationsQuery, NotificationsResponseDto>
    {
        private readonly INotificationsClient _notificationsClient;

        public GetNotificationsQueryHandler(INotificationsClient notificationsClient)
        {
            _notificationsClient = notificationsClient;
        }

        public async Task<Result<NotificationsResponseDto>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _notificationsClient.GetNotificationsAsync(
                    request.CompanyId,
                    request.PageSize,
                    request.PageNo,
                    request.DateFrom,
                    request.DateTo,
                    request.Type,
                    request.Language,
                    request.Status,
                    request.Channel
                );

                return result;
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<NotificationsResponseDto>("GetNotificationsQuery", $"Failed to get notifications: {ex.Message}");
            }
        }
    }
}
