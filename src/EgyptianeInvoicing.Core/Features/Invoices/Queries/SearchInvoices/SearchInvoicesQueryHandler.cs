using AutoMapper;
using BuildingBlocks.Extentions;
using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using EgyptianeInvoicing.Core.Models;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Invoices.Queries.SearchInvoices
{
    public class SearchInvoicesQueryHandler : IListQueryHandler<SearchInvoicesQuery, InvoiceDto>
    {
        private readonly IInvoiceRepository _repository;
        private readonly IMapper _mapper;

        public SearchInvoicesQueryHandler(IInvoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<CustomList<InvoiceDto>>> Handle(SearchInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _repository.GetByPaginationAsync(
               request.Options,
               request.InvoiceStatus,
               request.DocumentType,
               request.ReceiverType,
               request.ReceiverId,
               request.IssuerType,
               request.IssuerId,
               request.EInvoiceId,
               request.InternalID
           );

            var documentDtos = _mapper.Map<List<InvoiceDto>>(invoices);

            return Result.Success(documentDtos.ToCustomList(invoices.Count, invoices.Count));
        }
    }
}
