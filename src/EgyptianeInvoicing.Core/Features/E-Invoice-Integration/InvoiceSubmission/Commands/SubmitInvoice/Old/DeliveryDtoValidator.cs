using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using FluentValidation;

namespace EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice
{
    public class DeliveryDtoValidator : AbstractValidator<DeliveryDto>
    {
        public DeliveryDtoValidator()
        {
            RuleFor(delivery => delivery.Approach)
                .NotEmpty()
                .WithMessage("Approach must be provided.");

            RuleFor(delivery => delivery.Packaging)
                .NotEmpty()
                .WithMessage("Packaging must be provided.");

            RuleFor(delivery => delivery.DateValidity)
                .NotEmpty()
                .WithMessage("DateValidity must be provided.")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("DateValidity must be in the past or current.");

            RuleFor(delivery => delivery.ExportPort)
                .NotEmpty()
                .WithMessage("ExportPort must be provided.");

            RuleFor(delivery => delivery.GrossWeight)
                .GreaterThanOrEqualTo(0)
                .WithMessage("GrossWeight must be greater than or equal to 0.");

            RuleFor(delivery => delivery.NetWeight)
                .GreaterThanOrEqualTo(0)
                .WithMessage("NetWeight must be greater than or equal to 0.");

            RuleFor(delivery => delivery.Terms)
                .NotEmpty()
                .WithMessage("Terms must be provided.");
        }
    }
}
