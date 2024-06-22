using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentNotification.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentNotifications.Commands.ReceiveDocumentNotifications
{
    public class ReceiveDocumentNotificationsCommandValidator : AbstractValidator<ReceiveDocumentNotificationsCommand>
    {
        public ReceiveDocumentNotificationsCommandValidator()
        {
            RuleFor(x => x.DeliveryId).NotEmpty().WithMessage("DeliveryId is required.");
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required.");
            RuleFor(x => x.Count).GreaterThan(0).WithMessage("Count must be greater than zero.");
            RuleForEach(x => x.Messages).SetValidator(new DocumentNotificationMessageDtoValidator());
        }
    }

    public class DocumentNotificationMessageDtoValidator : AbstractValidator<DocumentNotificationMessageDto>
    {
        public DocumentNotificationMessageDtoValidator()
        {
            RuleFor(x => x.type).NotEmpty().WithMessage("Type is required.");
            RuleFor(x => x.uuid).NotEmpty().WithMessage("UUID is required.");
            RuleFor(x => x.submissionUUID).NotEmpty().WithMessage("SubmissionUUID is required.");
            RuleFor(x => x.longId).NotEmpty().WithMessage("LongId is required.");
            RuleFor(x => x.internalId).NotEmpty().WithMessage("InternalId is required.");
            RuleFor(x => x.status).NotEmpty().WithMessage("Status is required.");
        }
    }
}
