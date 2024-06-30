using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using FluentValidation;

namespace EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice
{
    public class DocumentDtoValidator : AbstractValidator<EInvoiceDto>
    {
        public DocumentDtoValidator()
        {
            RuleFor(doc => doc.Issuer)
                .NotNull()
                .SetValidator(new CompanyDtoValidator());

            RuleFor(doc => doc.Receiver)
                .NotNull()
                .SetValidator(new CompanyDtoValidator());

            RuleFor(doc => doc.DocumentTypeVersion)
                .NotEmpty()
                .Equal("1.0")
                .WithMessage("DocumentTypeVersion must be '1.0'.");

            RuleFor(doc => doc.DateTimeIssued)
                .NotEmpty()
                .Must(dateTimeString => DateTime.TryParse(dateTimeString, out var dateTime) && dateTime <= DateTime.UtcNow)
                .WithMessage("DateTimeIssued must be a valid date in the past or current.");


            RuleFor(doc => doc.TaxpayerActivityCode)
                .NotEmpty()
                .Matches(@"^\d{4}$")
                .WithMessage("TaxpayerActivityCode must be a 4-digit numeric string.");

            RuleFor(doc => doc.InternalID)
                .NotEmpty();

            RuleFor(doc => doc.TotalAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("TotalAmount must be greater than or equal to 0.");

            RuleFor(doc => doc.ExtraDiscountAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("ExtraDiscountAmount must be greater than or equal to 0.");

            RuleFor(doc => doc.TotalItemsDiscountAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("TotalItemsDiscountAmount must be greater than or equal to 0.");

            RuleForEach(doc => doc.Signatures)
                .SetValidator(new SignatureDtoValidator());

            RuleFor(doc => doc.Payment)
                .SetValidator(new PaymentDtoValidator())
                .When(doc => doc.Payment != null);

            RuleFor(doc => doc.Delivery)
                .SetValidator(new DeliveryDtoValidator())
                .When(doc => doc.Delivery != null);

            RuleFor(doc => doc.DocumentType)
                .NotEmpty()
                .MaximumLength(1)
                .Must(type => type == "i" || type == "c" || type == "d")
                .WithMessage("DocumentType must be 'i' for invoice, 'c' for credit note, or 'd' for debit note.");

            RuleFor(doc => doc.InvoiceLines)
                .Must((doc, lines) =>
                {
                    if (doc.DocumentType == "i" || doc.DocumentType == "c" || doc.DocumentType == "d")
                    {
                        return lines != null && lines.Count >= 1;
                    }
                    return true;
                })
                .WithMessage("At least one InvoiceLine is required.");

            RuleFor(doc => doc.References)
                .Must((doc, references) =>
                {
                    if (doc.DocumentType == "c" || doc.DocumentType == "d")
                    {
                        return references != null && references.Any();
                    }
                    return true;
                })
                .WithMessage("Credit note must reference at least one previous document.");

            RuleFor(doc => doc.TotalSalesAmount)
                .GreaterThanOrEqualTo(0)
                .When(doc => doc.DocumentType == "c" || doc.DocumentType == "d")
                .WithMessage("TotalSalesAmount must be greater than or equal to 0 for credit note or debit note.");

            RuleFor(doc => doc.TotalDiscountAmount)
                .GreaterThanOrEqualTo(0)
                .When(doc => doc.DocumentType == "c" || doc.DocumentType == "d")
                .WithMessage("TotalDiscountAmount must be greater than or equal to 0 for credit note or debit note.");

            RuleFor(doc => doc.NetAmount)
                .GreaterThanOrEqualTo(0)
                .When(doc => doc.DocumentType == "c" || doc.DocumentType == "d")
                .WithMessage("NetAmount must be greater than or equal to 0 for credit note or debit note.");

            RuleFor(doc => doc.TaxTotals)
                .Must((doc, taxTotals) =>
                {
                    if (doc.DocumentType == "c" || doc.DocumentType == "d")
                    {
                        return taxTotals != null && taxTotals.Any();
                    }
                    return true;
                })
                .WithMessage("At least one TaxTotal is required for credit note or debit note.")
                .ForEach(itemRule => itemRule.SetValidator(new TaxTotalDtoValidator()));
        }
    }
}
