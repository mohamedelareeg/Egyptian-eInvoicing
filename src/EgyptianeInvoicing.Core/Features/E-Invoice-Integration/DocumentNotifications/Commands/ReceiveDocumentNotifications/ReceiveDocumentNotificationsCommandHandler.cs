using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentNotifications.Commands.ReceiveDocumentNotifications
{
    public class ReceiveDocumentNotificationsCommandHandler : ICommandHandler<ReceiveDocumentNotificationsCommand, bool>
    {
        private readonly IDocumentNotificationClient _documentNotificationClient;

        public ReceiveDocumentNotificationsCommandHandler(IDocumentNotificationClient documentNotificationClient)
        {
            _documentNotificationClient = documentNotificationClient;
        }

        public async Task<Result<bool>> Handle(ReceiveDocumentNotificationsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _documentNotificationClient.ReceiveDocumentNotificationsAsync(
                    request.CompanyId,
                    request.DeliveryId,
                    request.Type,
                    request.Count,
                    request.Messages
                );

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Result.Failure<bool>("ReceiveDocumentNotificationsCommand", $"Failed: {errorMessage}");
                }

                return Result.Success(true);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<bool>("ReceiveDocumentNotificationsCommand", ex.Message);
            }
        }
    }
}
