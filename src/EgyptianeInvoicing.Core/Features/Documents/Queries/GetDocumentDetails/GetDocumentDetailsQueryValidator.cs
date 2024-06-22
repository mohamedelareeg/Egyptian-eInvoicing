using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Documents.Queries.GetDocumentDetails
{
    public class GetDocumentDetailsQueryValidator : AbstractValidator<GetDocumentDetailsQuery>
    {
        public GetDocumentDetailsQueryValidator()
        {
            RuleFor(x => x.DocumentUUID)
                .NotEmpty().WithMessage("DocumentUUID is required.");
        }
    }
}
