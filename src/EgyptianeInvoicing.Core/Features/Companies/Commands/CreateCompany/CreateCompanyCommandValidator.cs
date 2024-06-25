using EgyptianeInvoicing.Core.ValueObjects;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace EgyptianeInvoicing.Core.Features.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Company name is required.");
            RuleFor(x => x.Email)
                           .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                           .WithMessage("A valid email is required.");
            RuleFor(x => x.TaxNumber).NotEmpty().WithMessage("Tax number is required.");
            RuleFor(x => x.CommercialRegistrationNo).NotEmpty().WithMessage("Commercial registration number is required.");
            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address).SetValidator(new AddressValidator());
            });

            When(x => x.Payments != null, () =>
            {
                RuleForEach(x => x.Payments).SetValidator(new PaymentValidator());
            });

            When(x => x.Credentials != null, () =>
            {
                RuleFor(x => x.Credentials).NotNull().SetValidator(new ClientCredentialsValidator()); 
            });
        }

        // Validator for Address
        public class AddressValidator : AbstractValidator<AddressDto>
        {
            public AddressValidator()
            {
                RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.");
                RuleFor(x => x.Governorate).NotEmpty().WithMessage("Governate is required.");
                RuleFor(x => x.BranchId).MaximumLength(50).WithMessage("BranchId cannot exceed 50 characters.");
                RuleFor(x => x.RegionCity).MaximumLength(100).WithMessage("Region/City cannot exceed 100 characters.");
                RuleFor(x => x.Street).MaximumLength(100).WithMessage("Street cannot exceed 100 characters.");
                RuleFor(x => x.BuildingNumber).MaximumLength(20).WithMessage("Building Number cannot exceed 20 characters.");
                RuleFor(x => x.PostalCode).MaximumLength(20).WithMessage("Postal Code cannot exceed 20 characters.");
                RuleFor(x => x.Floor).MaximumLength(10).WithMessage("Floor cannot exceed 10 characters.");
                RuleFor(x => x.Room).MaximumLength(10).WithMessage("Room cannot exceed 10 characters.");
                RuleFor(x => x.Landmark).MaximumLength(100).WithMessage("Landmark cannot exceed 100 characters.");
                RuleFor(x => x.AdditionalInformation).MaximumLength(500).WithMessage("Additional Information cannot exceed 500 characters.");
            }
        }

        // Validator for ClientCredentials
        public class ClientCredentialsValidator : AbstractValidator<ClientCredentialsDto>
        {
            public ClientCredentialsValidator()
            {
                RuleFor(x => x.ClientId).NotEmpty().WithMessage("Client ID is required.");
                RuleFor(x => x.ClientSecret1).NotEmpty().WithMessage("Client Secret 1 is required.");
                RuleFor(x => x.ClientSecret2).NotEmpty().WithMessage("Client Secret 2 is required.");
                RuleFor(x => x.TokenPin).NotEmpty().WithMessage("Token Pin is required.");
                RuleFor(x => x.Certificate).MaximumLength(100).WithMessage("Certificate cannot exceed 100 characters.");
            }
        }

        // Validator for Payment
        public class PaymentValidator : AbstractValidator<PaymentDto>
        {
            public PaymentValidator()
            {
                RuleFor(x => x.BankName).MaximumLength(100).WithMessage("Bank Name cannot exceed 100 characters.");
                RuleFor(x => x.BankAddress).MaximumLength(200).WithMessage("Bank Address cannot exceed 200 characters.");
                RuleFor(x => x.BankAccountNo).MaximumLength(50).WithMessage("Bank Account Number cannot exceed 50 characters.");
                RuleFor(x => x.BankAccountIBAN).MaximumLength(50).WithMessage("Bank Account IBAN cannot exceed 50 characters.");
                RuleFor(x => x.SwiftCode).MaximumLength(50).WithMessage("Swift Code cannot exceed 50 characters.");
                RuleFor(x => x.Terms).MaximumLength(500).WithMessage("Terms cannot exceed 500 characters.");
            }
        }
    }
}
