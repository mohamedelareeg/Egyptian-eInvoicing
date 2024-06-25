using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentHandling.Commands.DeclineRejection
{
    public class DeclineRejectionCommandValidator : AbstractValidator<DeclineRejectionCommand>
    {
        public DeclineRejectionCommandValidator()
        {
            RuleFor(x => x.DocumentUUID)
                .NotEmpty().WithMessage("Document UUID is required.");
        }
    }
}
