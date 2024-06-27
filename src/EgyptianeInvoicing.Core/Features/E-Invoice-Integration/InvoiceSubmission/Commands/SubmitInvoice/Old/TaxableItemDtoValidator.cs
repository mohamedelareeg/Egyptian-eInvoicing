using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using FluentValidation;

namespace EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice
{
    public class TaxableItemDtoValidator : AbstractValidator<TaxableItemDto>
    {
        public TaxableItemDtoValidator()
        {
            RuleFor(item => item.TaxType).NotEmpty();
            RuleFor(item => item.Amount).GreaterThanOrEqualTo(0);
            RuleFor(item => item.SubType).NotEmpty();
            RuleFor(item => item.Rate).InclusiveBetween(0, 999);
        }
    }
}
