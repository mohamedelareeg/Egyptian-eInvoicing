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
        public ReceiveDocumentPackageNotificationCommand(Guid companyId, string deliveryId, string packageId)
        {
            CompanyId = companyId;
            DeliveryId = deliveryId;
            PackageId = packageId;
        }

        public Guid CompanyId { get; set; }
        public string DeliveryId { get; set; }
        public string PackageId { get; set; }
    }
}
