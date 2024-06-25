using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.RequestDocumentPackage.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentPackages.Queries.RequestDocumentPackage
{
    public class RequestDocumentPackageQueryValidator : AbstractValidator<DocumentPackageRequestDto>
    {
        public RequestDocumentPackageQueryValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(50).WithMessage("Type cannot exceed 50 characters.");

            RuleFor(x => x.Format)
                .NotEmpty().WithMessage("Format is required.")
                .MaximumLength(50).WithMessage("Format cannot exceed 50 characters.");

            RuleFor(x => x.TruncateIfExceeded)
                .NotNull().WithMessage("TruncateIfExceeded must be provided.");

            RuleFor(x => x.QueryParameters)
                .NotNull().WithMessage("QueryParameters must be provided.")
                .SetValidator(new DocumentPackageQueryParametersDtoValidator());
        }
        public class DocumentPackageQueryParametersDtoValidator : AbstractValidator<DocumentPackageQueryParametersDto>
        {
            public DocumentPackageQueryParametersDtoValidator()
            {
                RuleFor(x => x.DateFrom)
                    .NotEmpty().WithMessage("DateFrom is required.");

                RuleFor(x => x.DateTo)
                    .NotEmpty().WithMessage("DateTo is required.")
                    .GreaterThanOrEqualTo(x => x.DateFrom).WithMessage("DateTo must be greater than or equal to DateFrom.");

                RuleForEach(x => x.Statuses)
                    .NotEmpty().WithMessage("At least one Status must be provided.");

                RuleForEach(x => x.ProductsInternalCodes)
                    .NotEmpty().WithMessage("At least one ProductsInternalCode must be provided.");

                RuleFor(x => x.ReceiverSenderType)
                    .NotEmpty().WithMessage("ReceiverSenderType is required.");

                RuleFor(x => x.DocumentTypeNames)
                    .Must(x => x != null && x.Any()).WithMessage("At least one DocumentTypeName must be provided.");

                RuleFor(x => x.RepresentedTaxpayerFilterType)
                    .NotEmpty().WithMessage("RepresentedTaxpayerFilterType is required.");

                RuleFor(x => x.RepresenteeRin)
                    .NotEmpty().When(x => x.RepresentedTaxpayerFilterType == "E").WithMessage("RepresenteeRin is required when RepresentedTaxpayerFilterType is 'E'.");

                RuleFor(x => x.BranchNumber)
                    .NotEmpty().WithMessage("BranchNumber is required.");

                RuleForEach(x => x.ItemCodes)
                    .SetValidator(new DocumentPackageItemCodeDtoValidator());
            }
        }

        public class DocumentPackageItemCodeDtoValidator : AbstractValidator<DocumentPackageItemCodeDto>
        {
            public DocumentPackageItemCodeDtoValidator()
            {
                RuleFor(x => x.CodeValue)
                    .NotEmpty().WithMessage("CodeValue is required.")
                    .MaximumLength(50).WithMessage("CodeValue cannot exceed 50 characters.");

                RuleFor(x => x.CodeType)
                    .NotEmpty().WithMessage("CodeType is required.")
                    .MaximumLength(50).WithMessage("CodeType cannot exceed 50 characters.");
            }
        }
    }
}
