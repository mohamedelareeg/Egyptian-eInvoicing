using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using FluentValidation;

namespace EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice
{
    public class InvoiceLineDtoValidator : AbstractValidator<InvoiceLineDto>
    {
        public InvoiceLineDtoValidator()
        {
            RuleFor(line => line.Description)
                .NotEmpty()
                .WithMessage("Description must not be empty.");

            RuleFor(line => line.ItemType)
                .NotEmpty()
                .Must(type => type == "GS1" || type == "EGS")
                .WithMessage("ItemType must be 'GS1' or 'EGS'.");

            RuleFor(line => line.ItemCode)
                .NotEmpty()
                .WithMessage("ItemCode must not be empty.");

            RuleFor(line => line.UnitType)
                .NotEmpty()
                .WithMessage("UnitType must not be empty.");

            RuleFor(line => line.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");

            RuleFor(line => line.InternalCode)
                .NotEmpty()
                .WithMessage("InternalCode must not be empty.");

            RuleFor(line => line.SalesTotal)
                .GreaterThanOrEqualTo(0)
                .WithMessage("SalesTotal must be greater than or equal to 0.");

            RuleFor(line => line.Total)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total must be greater than or equal to 0.");

            RuleFor(line => line.ValueDifference)
                .GreaterThanOrEqualTo(0)
                .WithMessage("ValueDifference must be greater than or equal to 0.");

            RuleFor(line => line.TotalTaxableFees)
                .GreaterThanOrEqualTo(0)
                .WithMessage("TotalTaxableFees must be greater than or equal to 0.");

            RuleFor(line => line.NetTotal)
                .GreaterThanOrEqualTo(0)
                .WithMessage("NetTotal must be greater than or equal to 0.");

            RuleFor(line => line.ItemsDiscount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("ItemsDiscount must be greater than or equal to 0.");

            RuleFor(line => line.UnitValue)
                .NotNull()
                .SetValidator(new UnitValueDtoValidator())
                .WithMessage("UnitValue must be provided and valid.");

            RuleFor(line => line.Discount)
                .SetValidator(new DiscountDtoValidator())
                .When(line => line.Discount != null)
                .WithMessage("Discount must be valid if provided.");

            RuleForEach(line => line.TaxableItems)
                .SetValidator(new TaxableItemDtoValidator());
        }
    }
}
