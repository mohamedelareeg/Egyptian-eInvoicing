using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentOperations.Commands.RejectDocument
{
    public class RejectDocumentCommand : ICommand<bool>
    {
        public RejectDocumentCommand(Guid companyId, string documentUUID, string reason)
        {
            CompanyId = companyId;
            DocumentUUID = documentUUID;
            Reason = reason;
        }

        public Guid CompanyId { get; set; }
        public string DocumentUUID { get; set; }
        public string Reason { get; set; }
    }
}
