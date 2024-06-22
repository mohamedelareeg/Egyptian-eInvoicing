using FluentValidation;

namespace EgyptianeInvoicing.Core.Features.Codes.Queries.SearchCodeUsageRequests
{
    public class SearchCodeUsageRequestsQueryValidator : AbstractValidator<SearchCodeUsageRequestsQuery>
    {
        public SearchCodeUsageRequestsQueryValidator()
        {
            RuleFor(x => x.Active).NotEmpty().WithMessage("Active parameter is required.");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status parameter is required.");
            RuleFor(x => x.PageSize)
                .NotEmpty().WithMessage("PageSize parameter is required.")
                .Must(BeValidInteger).WithMessage("PageSize must be a valid integer greater than zero.");
            RuleFor(x => x.PageNumber)
                .NotEmpty().WithMessage("PageNumber parameter is required.")
                .Must(BeValidInteger).WithMessage("PageNumber must be a valid integer greater than zero.");
            RuleFor(x => x.OrderDirection).NotEmpty().WithMessage("OrderDirection parameter is required.");
        }

        private bool BeValidInteger(string value)
        {
            if (!int.TryParse(value, out int intValue))
            {
                return false;
            }
            return intValue > 0;
        }
    }
}
