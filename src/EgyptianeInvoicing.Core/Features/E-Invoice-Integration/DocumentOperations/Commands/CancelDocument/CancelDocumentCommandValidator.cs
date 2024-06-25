using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentOperations.Commands.CancelDocument
{
    public class CancelDocumentCommandValidator : AbstractValidator<CancelDocumentCommand>
    {
        public CancelDocumentCommandValidator()
        {
            RuleFor(x => x.DocumentUUID).NotEmpty().WithMessage("Document UUID is required.");
            RuleFor(x => x.Reason).NotEmpty().WithMessage("Cancellation reason is required.");
        }
    }
}
