using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentPackages.Queries.GetPackagesRequest
{
    public class GetPackagesRequestQueryValidator : AbstractValidator<GetPackagesRequestQuery>
    {
        public GetPackagesRequestQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("PageSize must be greater than zero.");

            RuleFor(x => x.PageNo)
                .GreaterThan(0).WithMessage("PageNo must be greater than zero.");

            RuleFor(x => x.DateFrom)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("DateFrom must be less than or equal to current UTC time.");

            RuleFor(x => x.DateTo)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("DateTo must be less than or equal to current UTC time.")
                .GreaterThanOrEqualTo(x => x.DateFrom).When(x => x.DateFrom != default).WithMessage("DateTo must be greater than or equal to DateFrom if DateFrom is provided.");

            RuleFor(x => x.DocumentTypeName)
                .MaximumLength(100).WithMessage("DocumentTypeName must not exceed 100 characters.");

            RuleFor(x => x.Statuses)
                .MaximumLength(100).WithMessage("Statuses must not exceed 100 characters.");

            RuleFor(x => x.ProductsInternalCodes)
                .MaximumLength(100).WithMessage("ProductsInternalCodes must not exceed 100 characters.");

            RuleFor(x => x.ReceiverSenderId)
                .MaximumLength(100).WithMessage("ReceiverSenderId must not exceed 100 characters.");

            RuleFor(x => x.BranchNumber)
                .MaximumLength(100).WithMessage("BranchNumber must not exceed 100 characters.");

            RuleFor(x => x.ItemCodes)
                .MaximumLength(100).WithMessage("ItemCodes must not exceed 100 characters.");
        }
    }
}
