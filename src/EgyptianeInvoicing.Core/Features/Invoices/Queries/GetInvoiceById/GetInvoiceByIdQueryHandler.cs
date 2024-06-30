using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using EgyptianeInvoicing.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Invoices.Queries.GetInvoiceById
{
    public class GetInvoiceByIdQueryHandler : IQueryHandler<GetInvoiceByIdQuery, Invoice>
    {
        private readonly IInvoiceRepository _repository;

        public GetInvoiceByIdQueryHandler(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Invoice>> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _repository.GetByIdAsync(request.Id);
            if (invoice == null)
                return Result.Failure<Invoice>("GetInvoiceByIdQuery", "Invoice not found.");

            return Result.Success(invoice);
        }
    }
}
