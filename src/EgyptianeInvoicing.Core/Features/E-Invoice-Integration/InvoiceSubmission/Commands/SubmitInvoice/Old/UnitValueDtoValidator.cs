using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using FluentValidation;

namespace EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice
{
    public class UnitValueDtoValidator : AbstractValidator<UnitValueDto>
    {
        public UnitValueDtoValidator()
        {
            RuleFor(value => value.CurrencySold)
                .NotEmpty()
                .WithMessage("CurrencySold must not be empty.");

            RuleFor(value => value.CurrencyExchangeRate)
                .NotEmpty()
                .When(value => value.CurrencySold != "EGP")
                .WithMessage("CurrencyExchangeRate is mandatory when CurrencySold is not EGP.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("CurrencyExchangeRate must be greater than or equal to 0.")
                .ScalePrecision(5, 5)
                .WithMessage("CurrencyExchangeRate must have a maximum of 5 decimal places.");
        }
    }
}
