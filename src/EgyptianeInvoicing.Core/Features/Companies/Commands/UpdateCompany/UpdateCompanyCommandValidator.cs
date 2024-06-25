using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EgyptianeInvoicing.Core.Features.Companies.Commands.CreateCompany.CreateCompanyCommandValidator;

namespace EgyptianeInvoicing.Core.Features.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Company name is required.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email address is required.")
                                  .EmailAddress().WithMessage("Email address must be valid.")
                                  .When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.TaxNumber).NotEmpty().WithMessage("Tax number is required.");
            RuleFor(x => x.CommercialRegistrationNo).NotEmpty().WithMessage("Commercial registration number is required.");
            RuleFor(x => x.Address).NotNull().WithMessage("Address is required.")
                                   .SetValidator(new AddressValidator());
            RuleFor(x => x.Credentials).NotNull().WithMessage("Credentials are required.")
                                       .SetValidator(new ClientCredentialsValidator());
        }
    }
}
