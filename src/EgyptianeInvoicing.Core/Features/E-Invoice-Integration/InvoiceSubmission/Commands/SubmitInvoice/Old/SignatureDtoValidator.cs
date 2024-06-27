using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using FluentValidation;

namespace EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice
{
    public class SignatureDtoValidator : AbstractValidator<SignatureDto>
    {
        public SignatureDtoValidator()
        {
            RuleFor(signature => signature.SignatureType)
                .NotEmpty()
                .Must(type => type == "I" || type == "S")
                .WithMessage("SignatureType must be 'I' or 'S'.");

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
