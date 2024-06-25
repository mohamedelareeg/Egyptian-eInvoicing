﻿using BuildingBlocks.Messaging;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentNotification.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentNotifications.Commands.ReceiveDocumentNotifications
{
    public class ReceiveDocumentNotificationsCommand : ICommand<bool>
    {
        public string DeliveryId { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
        public DocumentNotificationMessageDto[] Messages { get; set; }
    }
}