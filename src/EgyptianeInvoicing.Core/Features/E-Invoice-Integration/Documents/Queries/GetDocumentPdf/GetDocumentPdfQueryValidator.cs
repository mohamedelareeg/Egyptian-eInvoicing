using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Documents.Queries.GetDocumentPdf
{
    public class GetDocumentPdfQueryValidator : AbstractValidator<GetDocumentPdfQuery>
    {
        public GetDocumentPdfQueryValidator()
        {
            RuleFor(x => x.DocumentUUID)
                .NotEmpty().WithMessage("DocumentUUID is required.");
        }
    }
}
