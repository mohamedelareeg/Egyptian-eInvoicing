using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.UpdateCode.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Commands.UpdateCode
{
    public class UpdateCodeCommandValidator : AbstractValidator<UpdateCodeCommand>
    {
        public UpdateCodeCommandValidator()
        {
            RuleFor(x => x.CodeType).NotEmpty().WithMessage("CodeType is required.");
            RuleFor(x => x.ItemCode).NotEmpty().WithMessage("ItemCode is required.");
            RuleFor(x => x.Request).SetValidator(new CodeUpdateRequestDtoValidator());

        }
    }
    public class CodeUpdateRequestDtoValidator : AbstractValidator<CodeUpdateRequestDto>
    {
        public CodeUpdateRequestDtoValidator()
        {
            RuleFor(x => x.CodeDescriptionPrimaryLang)
                .NotEmpty().WithMessage("Primary language code description is required.")
                .MaximumLength(255).WithMessage("Primary language code description must not exceed 255 characters.");

            RuleFor(x => x.CodeDescriptionSecondaryLang)
                .MaximumLength(255).WithMessage("Secondary language code description must not exceed 255 characters.");

            RuleFor(x => x.ActiveTo)
                .GreaterThanOrEqualTo(DateTime.Today).When(x => x.ActiveTo.HasValue)
                .WithMessage("Active to date cannot be in the past.");

            RuleFor(x => x.LinkedCode)
                .MaximumLength(50).WithMessage("Linked code must not exceed 50 characters.");
        }
    }
}
