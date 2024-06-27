using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EgyptianeInvoicing.Core.Features.DocumentOperations.Commands.CancelDocument
{
    public class CancelDocumentCommand : ICommand<bool>
    {
        public CancelDocumentCommand(Guid companyId, string documentUUID, string reason)
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
