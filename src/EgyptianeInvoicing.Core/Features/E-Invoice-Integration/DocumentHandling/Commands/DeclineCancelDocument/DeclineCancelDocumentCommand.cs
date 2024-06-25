using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentHandling.Commands.DeclineCancelDocument
{
    public class DeclineCancelDocumentCommand : ICommand<bool>
    {
        public string DocumentUUID { get; }
        public string DeclineReason { get; }

        public DeclineCancelDocumentCommand(string documentUUID, string declineReason)
        {
            DocumentUUID = documentUUID;
            DeclineReason = declineReason;
        }
    }
}
