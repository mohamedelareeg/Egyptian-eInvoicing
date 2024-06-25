using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentOperations.Queries.GetRecentDocuments
{
    public class GetRecentDocumentsQueryValidator : AbstractValidator<GetRecentDocumentsQuery>
    {
        public GetRecentDocumentsQueryValidator()
        {
            RuleFor(x => x.SubmissionDateFrom).NotEmpty().WithMessage("Submission date from is required.");
            RuleFor(x => x.SubmissionDateTo).NotEmpty().WithMessage("Submission date to is required.")
                                              .GreaterThan(x => x.SubmissionDateFrom).WithMessage("Submission date to must be greater than submission date from.");

            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page size must be greater than zero.");
            RuleFor(x => x.PageNo).GreaterThan(0).WithMessage("Page number must be greater than zero.");

            //RuleFor(x => x.IssueDateFrom).NotEmpty().When(x => !string.IsNullOrEmpty(x.IssueDateTo)).WithMessage("Issue date from is required when issue date to is specified.");
            //RuleFor(x => x.IssueDateTo).NotEmpty().When(x => !string.IsNullOrEmpty(x.IssueDateFrom)).WithMessage("Issue date to is required when issue date from is specified.");

            //RuleFor(x => x.DocumentType).NotEmpty().WithMessage("Document type is required.");
            //RuleFor(x => x.DocumentType).IsInEnum().WithMessage("Document type is not valid.");

            //RuleFor(x => x.ReceiverType).NotEmpty().When(x => !string.IsNullOrEmpty(x.ReceiverId)).WithMessage("Receiver type is required when receiver ID is specified.");
            //RuleFor(x => x.ReceiverId).NotEmpty().When(x => !string.IsNullOrEmpty(x.ReceiverType)).WithMessage("Receiver ID is required when receiver type is specified.");

            //RuleFor(x => x.IssuerType).NotEmpty().When(x => !string.IsNullOrEmpty(x.IssuerId)).WithMessage("Issuer type is required when issuer ID is specified.");
            //RuleFor(x => x.IssuerId).NotEmpty().When(x => !string.IsNullOrEmpty(x.IssuerType)).WithMessage("Issuer ID is required when issuer type is specified.");

        }
    }
}
