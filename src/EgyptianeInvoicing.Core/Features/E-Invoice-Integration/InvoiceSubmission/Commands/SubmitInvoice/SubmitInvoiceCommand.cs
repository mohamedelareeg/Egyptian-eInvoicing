using BuildingBlocks.Messaging;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Response;
using Net.Pkcs11Interop.HighLevelAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice
{
    public class SubmitInvoiceCommand : ICommand<SubmissionResponseDto>
    {
        public SubmitInvoiceCommand(Guid companyId, List<ImportedInvoiceDto> request, IObjectHandle certificate = null, X509Certificate2 certForSigning = null)
        {
            CompanyId = companyId;
            Request = request;
            this.certificate = certificate;
            this.certForSigning = certForSigning;
        }
        public IObjectHandle certificate { get; set; }
        public X509Certificate2 certForSigning { get; set; }
        public Guid CompanyId { get; set; }
        public List<ImportedInvoiceDto> Request { get; set; }
    }
}
