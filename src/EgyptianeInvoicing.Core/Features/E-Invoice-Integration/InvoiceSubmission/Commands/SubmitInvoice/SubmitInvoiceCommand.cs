using BuildingBlocks.Messaging;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice
{
    public class SubmitInvoiceCommand : ICommand<SubmissionResponseDto>
    {
        public SubmitInvoiceCommand(Guid companyId, List<ImportedInvoiceDto> request)
        {
            CompanyId = companyId;
            Request = request;
        }

        public Guid CompanyId { get; set; }
        public List<ImportedInvoiceDto> Request { get; set; }
    }
}
