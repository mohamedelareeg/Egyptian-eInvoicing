using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Invoices.Queries.GetInvoiceById
{
    public class GetInvoiceByIdQuery : IQuery<Invoice>
    {
        public Guid Id { get; set; }
    }
}
