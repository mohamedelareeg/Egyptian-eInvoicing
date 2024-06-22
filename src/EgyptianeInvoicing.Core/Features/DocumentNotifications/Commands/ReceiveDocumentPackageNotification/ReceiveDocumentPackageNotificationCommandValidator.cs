using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentNotifications.Commands.ReceiveDocumentPackageNotification
{
    public class ReceiveDocumentPackageNotificationCommandValidator : AbstractValidator<ReceiveDocumentPackageNotificationCommand>
    {
        public ReceiveDocumentPackageNotificationCommandValidator()
        {
            RuleFor(x => x.DeliveryId).NotEmpty().WithMessage("DeliveryId is required.");
            RuleFor(x => x.PackageId).NotEmpty().WithMessage("PackageId is required.");
        }
    }
}
