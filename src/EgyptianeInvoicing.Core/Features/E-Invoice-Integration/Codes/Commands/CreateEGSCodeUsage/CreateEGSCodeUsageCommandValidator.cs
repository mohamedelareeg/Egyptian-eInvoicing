using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.CreateEGSCode.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Commands.CreateEGSCodeUsage
{
    public class CreateEGSCodeUsageCommandValidator : AbstractValidator<CreateEGSCodeUsageCommand>
    {
        public CreateEGSCodeUsageCommandValidator()
        {
            RuleFor(command => command.Request)
                .NotEmpty().WithMessage("Request cannot be empty.");

            RuleForEach(command => command.Request).SetValidator(new CreateEGSCodeUsageItemDtoValidator());
        }
    }

    public class CreateEGSCodeUsageItemDtoValidator : AbstractValidator<CreateEGSCodeUsageItemDto>
    {
        public CreateEGSCodeUsageItemDtoValidator()
        {
            RuleFor(x => x.CodeType)
                .NotEmpty().WithMessage("CodeType is required.");

            RuleFor(x => x.ItemCode)
                .NotEmpty().WithMessage("Code is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");
        }
    }
}
