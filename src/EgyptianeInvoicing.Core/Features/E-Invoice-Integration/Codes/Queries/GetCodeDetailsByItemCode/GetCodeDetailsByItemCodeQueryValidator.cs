using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Queries.GetCodeDetailsByItemCode
{
    public class GetCodeDetailsByItemCodeQueryValidator : AbstractValidator<GetCodeDetailsByItemCodeQuery>
    {
        public GetCodeDetailsByItemCodeQueryValidator()
        {
            RuleFor(x => x.CodeType).NotEmpty().WithMessage("CodeType parameter is required.");
            RuleFor(x => x.ItemCode).NotEmpty().WithMessage("ItemCode parameter is required.");
        }
    }
}
