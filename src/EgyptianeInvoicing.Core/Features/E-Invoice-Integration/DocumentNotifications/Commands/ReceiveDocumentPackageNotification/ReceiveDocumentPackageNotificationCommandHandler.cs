using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentNotifications.Commands.ReceiveDocumentPackageNotification
{
    public class ReceiveDocumentPackageNotificationCommandHandler : ICommandHandler<ReceiveDocumentPackageNotificationCommand, bool>
    {
        private readonly IDocumentNotificationClient _documentNotificationClient;

        public ReceiveDocumentPackageNotificationCommandHandler(IDocumentNotificationClient documentNotificationClient)
        {
            _documentNotificationClient = documentNotificationClient;
        }

        public async Task<Result<bool>> Handle(ReceiveDocumentPackageNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _documentNotificationClient.ReceiveDocumentPackageNotificationAsync(
                    request.CompanyId,
                    request.DeliveryId,
                    request.PackageId
                );

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Result.Failure<bool>("ReceiveDocumentPackageNotificationCommand", $"Failed: {errorMessage}");
                }

                return Result.Success(true);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<bool>("ReceiveDocumentPackageNotificationCommand", ex.Message);
            }
        }
    }
}
