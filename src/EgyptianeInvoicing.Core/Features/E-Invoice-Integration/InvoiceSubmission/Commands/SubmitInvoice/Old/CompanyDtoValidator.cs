using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using FluentValidation;

namespace EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice
{
    public class CompanyDtoValidator : AbstractValidator<CompanyDto>
    {
        public CompanyDtoValidator()
        {
            RuleFor(company => company.Type)
                .NotEmpty()
                .Must(type => type == "B" || type == "P" || type == "F")
                .WithMessage("Type must be 'B', 'P', or 'F'.");

            RuleFor(company => company.Id)
                .NotEmpty()
                .When(company => company.Type == "B") // Registration number is mandatory for type 'B'
                .WithMessage("Id must be provided for business type 'B'.");

            RuleFor(company => company.Name)
                .NotEmpty()
                .WithMessage("Name must be provided.");

            RuleFor(company => company.Address)
                .SetValidator(new AddressDtoValidator())
                .When(company => company.Address != null);
        }
    }
}
