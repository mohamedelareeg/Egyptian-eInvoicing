using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.RequestCodeReuse.Response;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Commands.RequestCodeReuse
{
    public class RequestCodeReuseCommandValidator : AbstractValidator<RequestCodeReuseCommand>
    {
        public RequestCodeReuseCommandValidator()
        {
            RuleFor(x => x.Request).NotEmpty().WithMessage("Request cannot be empty.");
            RuleForEach(x => x.Request).SetValidator(new CodeUsageItemDtoValidator());
        }
    }

    public class CodeUsageItemDtoValidator : AbstractValidator<CodeUsageItemDto>
    {
        public CodeUsageItemDtoValidator()
        {
            RuleFor(x => x.CodeType).NotEmpty().WithMessage("CodeType is required.");
            RuleFor(x => x.ItemCode).NotEmpty().WithMessage("ItemCode is required.");
        }
    }
}
