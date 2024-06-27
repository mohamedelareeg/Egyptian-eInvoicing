using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentOperations.Queries.SearchDocuments
{
    public class SearchDocumentsQueryHandler : IQueryHandler<SearchDocumentsQuery, RecentDocumentsDto>
    {
        private readonly IDocumentOperationsClient _documentOperationsClient;

        public SearchDocumentsQueryHandler(IDocumentOperationsClient documentOperationsClient)
        {
            _documentOperationsClient = documentOperationsClient;
        }

        public async Task<Result<RecentDocumentsDto>> Handle(SearchDocumentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _documentOperationsClient.SearchDocumentsAsync(
                    request.CompanyId,
                    request.SubmissionDateFrom,
                    request.SubmissionDateTo,
                    request.IssueDateFrom,
                    request.IssueDateTo,
                    request.ContinuationToken,
                    request.PageSize,
                    request.Direction,
                    request.Status,
                    request.DocumentType,
                    request.ReceiverType,
                    request.ReceiverId,
                    request.IssuerType,
                    request.IssuerId,
                    request.UUID,
                    request.InternalID
                );

                return Result.Success(result);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<RecentDocumentsDto>("SearchDocumentsQuery", $"Failed to search documents: {ex.Message}");
            }
        }
    }
}
