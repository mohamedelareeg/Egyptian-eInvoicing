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

namespace EgyptianeInvoicing.Core.Features.DocumentOperations.Queries.GetRecentDocuments
{
    public class GetRecentDocumentsQueryHandler : IQueryHandler<GetRecentDocumentsQuery, RecentDocumentsDto>
    {
        private readonly IDocumentOperationsClient _documentOperationsClient;

        public GetRecentDocumentsQueryHandler(IDocumentOperationsClient documentOperationsClient)
        {
            _documentOperationsClient = documentOperationsClient;
        }

        public async Task<Result<RecentDocumentsDto>> Handle(GetRecentDocumentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _documentOperationsClient.GetRecentDocumentsAsync(
                    request.SubmissionDateFrom,
                    request.SubmissionDateTo,
                    request.IssueDateFrom,
                    request.IssueDateTo,
                    request.PageSize,
                    request.PageNo,
                    request.Direction,
                    request.Status,
                    request.DocumentType,
                    request.ReceiverType,
                    request.ReceiverId,
                    request.IssuerType,
                    request.IssuerId
                );

                return Result.Success(result);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<RecentDocumentsDto>("GetRecentDocumentsQuery", $"Failed to get recent documents: {ex.Message}");
            }
        }
    }
}
