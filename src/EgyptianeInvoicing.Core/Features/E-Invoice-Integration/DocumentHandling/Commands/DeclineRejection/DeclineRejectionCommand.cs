using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentHandling.Commands.DeclineRejection
{
    public class DeclineRejectionCommand : ICommand<bool>
    {
        public DeclineRejectionCommand(string documentUUID, Guid companyId)
        {
            DocumentUUID = documentUUID;
            CompanyId = companyId;
        }

        public string DocumentUUID { get; }
        public Guid CompanyId { get; set; }
    }
}
