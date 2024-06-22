using BuildingBlocks.Messaging;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice
{
    public class SubmitInvoiceCommand : ICommand<Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Response.SubmissionResponseDto>
    {
        public List<DocumentDto> Request { get; set; }
    }
}
