using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Invoices.Commands.DeleteInvoice
{
    public class DeleteInvoiceCommand : ICommand<bool>
    {
        public Guid Id { get; set; }
    }
}
