using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Commands.UpdateEGSCodeUsage
{
    public class UpdateEGSCodeUsageCommandValidator : AbstractValidator<UpdateEGSCodeUsageCommand>
    {
        public UpdateEGSCodeUsageCommandValidator()
        {
            RuleFor(x => x.CodeUsageRequestId).NotEmpty().WithMessage("CodeUsageRequestId is required.");
            RuleFor(x => x.Request).NotNull().WithMessage("Update request is required.");
        }
    }
}
