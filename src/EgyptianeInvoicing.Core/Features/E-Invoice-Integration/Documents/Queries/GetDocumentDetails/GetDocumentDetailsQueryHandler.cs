using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocumentDetails.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Documents.Queries.GetDocumentDetails
{
    public class GetDocumentDetailsQueryHandler : IQueryHandler<GetDocumentDetailsQuery, DocumentDetailsDto>
    {
        private readonly IDocumentRetrievalClient _client;

        public GetDocumentDetailsQueryHandler(IDocumentRetrievalClient client)
        {
            _client = client;
        }

        public async Task<Result<DocumentDetailsDto>> Handle(GetDocumentDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var details = await _client.GetDocumentDetailsAsync(request.CompanyId, request.DocumentUUID);
                return Result.Success(details);
            }
            catch (Exception ex)
            {
                return Result.Failure<DocumentDetailsDto>("GetDocumentDetailsQuery", $"Failed to get document details: {ex.Message}");
            }
        }
    }
}
