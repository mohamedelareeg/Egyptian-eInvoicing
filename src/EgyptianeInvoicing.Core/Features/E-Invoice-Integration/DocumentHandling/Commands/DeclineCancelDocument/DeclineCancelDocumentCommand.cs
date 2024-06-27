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
        public DeclineCancelDocumentCommand(Guid companyId, string documentUUID, string declineReason)
        {
            CompanyId = companyId;
            DocumentUUID = documentUUID;
            DeclineReason = declineReason;
        }

        public Guid CompanyId { get; set; }
        public string DocumentUUID { get; }
        public string DeclineReason { get; }

   
    }
}
