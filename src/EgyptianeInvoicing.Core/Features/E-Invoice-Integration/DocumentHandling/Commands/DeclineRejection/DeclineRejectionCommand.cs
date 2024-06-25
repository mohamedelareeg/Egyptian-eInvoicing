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
        public string DocumentUUID { get; }

        public DeclineRejectionCommand(string documentUUID)
        {
            DocumentUUID = documentUUID;
        }
    }
}
