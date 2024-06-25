using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocument.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Documents.Queries.GetDocument
{
    public class GetDocumentQueryHandler : IQueryHandler<GetDocumentQuery, DocumentRetrievalDto>
    {
        private readonly IDocumentRetrievalClient _client;

        public GetDocumentQueryHandler(IDocumentRetrievalClient client)
        {
            _client = client;
        }

        public async Task<Result<DocumentRetrievalDto>> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var document = await _client.GetDocumentAsync(request.DocumentUUID);
                return Result.Success(document);
            }
            catch (Exception ex)
            {
                return Result.Failure<DocumentRetrievalDto>("GetDocumentQuery", $"Failed to get document: {ex.Message}");
            }
        }
    }
}
