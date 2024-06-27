using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using FluentValidation;

namespace EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice
{
    public class PaymentDtoValidator : AbstractValidator<PaymentDto>
    {
        public PaymentDtoValidator()
        {
            RuleFor(payment => payment.BankName)
                .NotEmpty()
                .WithMessage("BankName must be provided.");

            RuleFor(payment => payment.BankAddress)
                .NotEmpty()
                .WithMessage("BankAddress must be provided.");

            RuleFor(payment => payment.BankAccountNo)
                .NotEmpty()
                .WithMessage("BankAccountNo must be provided.");

            RuleFor(payment => payment.Terms)
                .NotEmpty()
                .WithMessage("Terms must be provided.");
        }
    }
}
