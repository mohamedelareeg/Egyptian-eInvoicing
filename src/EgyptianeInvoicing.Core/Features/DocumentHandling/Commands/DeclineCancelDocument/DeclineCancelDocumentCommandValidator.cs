using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentHandling.Commands.DeclineCancelDocument
{
    public class DeclineCancelDocumentCommandValidator : AbstractValidator<DeclineCancelDocumentCommand>
    {
        public DeclineCancelDocumentCommandValidator()
        {
            RuleFor(x => x.DocumentUUID)
                .NotEmpty().WithMessage("Document UUID is required.");

            RuleFor(x => x.DeclineReason)
                .NotEmpty().WithMessage("Decline reason is required.")
                .MaximumLength(100).WithMessage("Decline reason must not exceed 100 characters.");
        }
    }
}
