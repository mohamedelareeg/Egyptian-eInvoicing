using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using FluentValidation;

namespace EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice
{
    public class TaxTotalDtoValidator : AbstractValidator<TaxTotalDto>
    {
        public TaxTotalDtoValidator()
        {
            RuleFor(total => total.TaxType).NotEmpty();
            RuleFor(total => total.Amount).GreaterThanOrEqualTo(0);
        }
    }
}
