using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentOperations.Commands.RejectDocument
{
    public class RejectDocumentCommandValidator : AbstractValidator<RejectDocumentCommand>
    {
        public RejectDocumentCommandValidator()
        {
            RuleFor(x => x.DocumentUUID).NotEmpty().WithMessage("Document UUID is required.");
            RuleFor(x => x.Reason).NotEmpty().WithMessage("Rejection reason is required.");
        }
    }
}

