using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice
{
    public class SubmitInvoiceValidator : AbstractValidator<SubmitInvoiceCommand>
    {
        public SubmitInvoiceValidator()
        {
            RuleFor(command => command.Request).NotEmpty().WithMessage("Request cannot be empty.");
            RuleForEach(command => command.Request).SetValidator(new DocumentDtoValidator());
        }
    }
    public class DocumentDtoValidator : AbstractValidator<DocumentDto>
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
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("DateTimeIssued must be in the past or current.");

            RuleFor(doc => doc.TaxpayerActivityCode)
                .NotEmpty()
                .Matches(@"^\d{4}$")
                .WithMessage("TaxpayerActivityCode must be a 4-digit numeric string.");

            RuleFor(doc => doc.InternalId)
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

    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(address => address.BranchId)
                .NotEmpty()
                .When(address => !string.IsNullOrEmpty(address.BranchId))
                .WithMessage("BranchId must be provided.");

            RuleFor(address => address.Country)
                .NotEmpty()
                .Equal("EG")
                .WithMessage("Country must be 'EG' for internal business issuers.");

            RuleFor(address => address.Governorate)
                .NotEmpty()
                .WithMessage("Governorate must be provided.");

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
    public class UnitValueDtoValidator : AbstractValidator<UnitValueDto>
    {
        public UnitValueDtoValidator()
        {
            RuleFor(value => value.CurrencySold)
                .NotEmpty()
                .WithMessage("CurrencySold must not be empty.");

            RuleFor(value => value.AmountEGP)
                .GreaterThanOrEqualTo(0)
                .WithMessage("AmountEGP must be greater than or equal to 0.")
                .ScalePrecision(5, 5)
                .WithMessage("AmountEGP must have a maximum of 5 decimal places.");

            RuleFor(value => value.AmountSold)
                .NotEmpty()
                .When(value => value.CurrencySold != "EGP")
                .WithMessage("AmountSold is mandatory when CurrencySold is not EGP.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("AmountSold must be greater than or equal to 0.")
                .ScalePrecision(5, 5)
                .WithMessage("AmountSold must have a maximum of 5 decimal places.");

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

    public class DiscountDtoValidator : AbstractValidator<DiscountDto>
    {
        public DiscountDtoValidator()
        {
            RuleFor(discount => discount.Rate).InclusiveBetween(0, 100).When(discount => discount.Rate > 0);
            RuleFor(discount => discount.Amount).GreaterThanOrEqualTo(0).When(discount => discount.Amount > 0);
        }
    }

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
    public class TaxTotalDtoValidator : AbstractValidator<TaxTotalDto>
    {
        public TaxTotalDtoValidator()
        {
            RuleFor(total => total.TaxType).NotEmpty();
            RuleFor(total => total.Amount).GreaterThanOrEqualTo(0);
        }
    }

    public class SignatureDtoValidator : AbstractValidator<SignatureDto>
    {
        public SignatureDtoValidator()
        {
            RuleFor(signature => signature.Type)
                .NotEmpty()
                .Must(type => type == "I" || type == "S")
                .WithMessage("Type must be 'I' or 'S'.");

            RuleFor(signature => signature.Value)
                .NotEmpty()
                .Must(BeAValidBase64String)
                .WithMessage("Value must be a valid Base64 encoded string.");
        }

        private bool BeAValidBase64String(string value)
        {
            try
            {
                Convert.FromBase64String(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
