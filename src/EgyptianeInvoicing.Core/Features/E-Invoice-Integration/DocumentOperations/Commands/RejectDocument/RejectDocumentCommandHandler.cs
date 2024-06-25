using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentOperations.Commands.RejectDocument
{
    public class RejectDocumentCommandHandler : ICommandHandler<RejectDocumentCommand, bool>
    {
        private readonly IDocumentOperationsClient _documentOperationsClient;

        public RejectDocumentCommandHandler(IDocumentOperationsClient documentOperationsClient)
        {
            _documentOperationsClient = documentOperationsClient;
        }

        public async Task<Result<bool>> Handle(RejectDocumentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _documentOperationsClient.RejectDocumentAsync(request.DocumentUUID, request.Reason);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Result.Failure<bool>("RejectDocumentCommand", $"Failed: {errorMessage}");
                }

                return Result.Success(true);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<bool>("RejectDocumentCommand", $"Failed to reject document: {ex.Message}");
            }
        }
    }
}
