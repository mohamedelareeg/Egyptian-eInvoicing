using BuildingBlocks.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentNotifications.Commands.ReceiveDocumentPackageNotification
{
    public class ReceiveDocumentPackageNotificationCommand : ICommand<bool>
    {
        public string DeliveryId { get; set; }
        public string PackageId { get; set; }
    }
}
