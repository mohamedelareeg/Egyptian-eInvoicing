using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentPackages.Queries.GetDocumentPackage
{
    public class GetDocumentPackageQueryValidator : AbstractValidator<GetDocumentPackageQuery>
    {
        public GetDocumentPackageQueryValidator()
        {
            RuleFor(x => x.Rid)
                .NotEmpty().WithMessage("Rid cannot be empty.");
        }
    }
}
