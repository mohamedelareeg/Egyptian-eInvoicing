using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using FluentValidation;

namespace EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice
{
    public class DiscountDtoValidator : AbstractValidator<DiscountDto>
    {
        public DiscountDtoValidator()
        {
            RuleFor(discount => discount.Rate).InclusiveBetween(0, 100).When(discount => discount.Rate > 0);
            RuleFor(discount => discount.Amount).GreaterThanOrEqualTo(0).When(discount => discount.Amount > 0);
        }
    }
}
