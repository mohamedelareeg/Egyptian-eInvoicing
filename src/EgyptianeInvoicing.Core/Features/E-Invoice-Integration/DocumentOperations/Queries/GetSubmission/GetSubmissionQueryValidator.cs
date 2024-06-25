using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentOperations.Queries.GetSubmission
{
    public class GetSubmissionQueryValidator : AbstractValidator<GetSubmissionQuery>
    {
        public GetSubmissionQueryValidator()
        {
            RuleFor(x => x.SubmissionUUID).NotEmpty().WithMessage("Submission UUID is required.");
            RuleFor(x => x.PageSize).NotEmpty().WithMessage("Page size is required.");
            RuleFor(x => x.PageNo).NotEmpty().WithMessage("Page number is required.");
            RuleFor(x => x.PageSize).Must(BeValidInteger).WithMessage("Page size must be a valid integer.");
            RuleFor(x => x.PageNo).Must(BeValidInteger).WithMessage("Page number must be a valid integer.");
        }

        private bool BeValidInteger(string value)
        {
            return int.TryParse(value, out _);
        }
    }
}
