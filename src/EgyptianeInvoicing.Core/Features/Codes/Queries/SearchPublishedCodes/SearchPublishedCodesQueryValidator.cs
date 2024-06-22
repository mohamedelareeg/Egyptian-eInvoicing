using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Queries.SearchPublishedCodes
{
    public class SearchPublishedCodesQueryValidator : AbstractValidator<SearchPublishedCodesQuery>
    {
        public SearchPublishedCodesQueryValidator()
        {
            RuleFor(x => x.CodeType).NotEmpty().WithMessage("CodeType parameter is required.");
            RuleFor(x => x.ParentLevelName).NotEmpty().WithMessage("ParentLevelName parameter is required.");
            RuleFor(x => x.ActiveFrom).NotEmpty().WithMessage("ActiveFrom parameter is required.");
            RuleFor(x => x.PageSize).NotEmpty().WithMessage("PageSize parameter is required.")
                                     .GreaterThan(0).WithMessage("PageSize must be greater than zero.");
            RuleFor(x => x.PageNumber).NotEmpty().WithMessage("PageNumber parameter is required.")
                                      .GreaterThan(0).WithMessage("PageNumber must be greater than zero.");
        }
    }
}
