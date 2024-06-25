using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentOperations.Queries.SearchDocuments
{
    public class SearchDocumentsQueryValidator : AbstractValidator<SearchDocumentsQuery>
    {
        public SearchDocumentsQueryValidator()
        {
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page size must be greater than zero.");

            RuleFor(x => x.SubmissionDateFrom).NotEmpty().When(x => x.SubmissionDateTo.HasValue).WithMessage("Submission date from is required when submission date to is specified.");
            RuleFor(x => x.SubmissionDateTo).NotEmpty().When(x => x.SubmissionDateFrom.HasValue).WithMessage("Submission date to is required when submission date from is specified.");

        }
    }
}
