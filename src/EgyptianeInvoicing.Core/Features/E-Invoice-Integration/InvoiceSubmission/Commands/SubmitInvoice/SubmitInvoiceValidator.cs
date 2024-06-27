using EgyptianeInvoicing.Shared.Dtos;
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
            RuleFor(command => command.CompanyId)
               .NotEmpty().WithMessage("CompanyId is required.");
            RuleFor(command => command.Request).NotEmpty().WithMessage("Request cannot be empty.");
            RuleForEach(command => command.Request).SetValidator(new ImportedInvoiceDtoValidator());
        }
    }
    public class ImportedInvoiceDtoValidator : AbstractValidator<ImportedInvoiceDto>
    {
        public ImportedInvoiceDtoValidator()
        {
            RuleFor(invoice => invoice.SerialNumber)
                .NotEmpty().WithMessage("Serial Number is required.");

            RuleFor(invoice => invoice.IssueDate)
                .NotEmpty().WithMessage("Issue Date is required.");

            RuleFor(invoice => invoice.InvoiceType)
                .NotEmpty().WithMessage("Invoice Type is required.");

            RuleFor(invoice => invoice.CustomerType)
                .NotEmpty().WithMessage("Customer Type is required.");

            RuleFor(invoice => invoice.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(invoice => invoice.RegisterNumber)
                .NotEmpty().WithMessage("Register Number is required.");

            RuleFor(invoice => invoice.Country)
                .NotEmpty().WithMessage("Country is required.");

            RuleForEach(invoice => invoice.Items)
                .SetValidator(new ImportedInvoiceItemDtoValidator());
        }
    }

    public class ImportedInvoiceItemDtoValidator : AbstractValidator<ImportedInvoiceItemDto>
    {
        public ImportedInvoiceItemDtoValidator()
        {
            RuleFor(item => item.ProductName)
                .NotEmpty().WithMessage("Product Name is required.");

            RuleFor(item => item.CodeType)
                .NotEmpty().WithMessage("Code Type is required.");

            RuleFor(item => item.Unit)
                .NotEmpty().WithMessage("Unit is required.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0).WithMessage("Unit Price must be greater than 0.");

            RuleFor(item => item.Currency)
                .NotEmpty().WithMessage("Currency is required.");

            RuleFor(item => item.VATPercentage)
                .GreaterThanOrEqualTo(0).WithMessage("VAT Percentage must be greater than or equal to 0.");

            RuleFor(item => item.RelativeTableTaxPercentage)
                .GreaterThanOrEqualTo(0).WithMessage("Relative Table Tax Percentage must be greater than or equal to 0.");

            RuleFor(item => item.SpecificTableTaxPercentage)
                .GreaterThanOrEqualTo(0).WithMessage("Specific Table Tax Percentage must be greater than or equal to 0.");

            RuleFor(item => item.DiscountTaxPercentage)
                .GreaterThanOrEqualTo(0).WithMessage("Discount Tax Percentage must be greater than or equal to 0.");
        }
    }
}
