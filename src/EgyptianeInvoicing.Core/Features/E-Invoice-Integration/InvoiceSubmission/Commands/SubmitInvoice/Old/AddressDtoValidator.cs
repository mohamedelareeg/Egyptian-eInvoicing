using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using FluentValidation;

namespace EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice
{
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(address => address.BranchID)
                .NotEmpty()
                .When(address => !string.IsNullOrEmpty(address.BranchID.ToString()))
                .WithMessage("BranchId must be provided.");

            RuleFor(address => address.Country)
                .NotEmpty()
                .Equal("EG")
                .WithMessage("Country must be 'EG' for internal business issuers.");

            RuleFor(address => address.Governate)
                .NotEmpty()
                .WithMessage("Governate must be provided.");

            RuleFor(address => address.RegionCity)
                .NotEmpty()
                .WithMessage("RegionCity must be provided.");

            RuleFor(address => address.Street)
                .NotEmpty()
                .WithMessage("Street must be provided.");

            RuleFor(address => address.BuildingNumber)
                .NotEmpty()
                .WithMessage("BuildingNumber must be provided.");
        }
    }
}
