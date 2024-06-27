using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentOperations.Commands.CancelDocument
{
    public class CancelDocumentCommandHandler : ICommandHandler<CancelDocumentCommand, bool>
    {
        private readonly IDocumentOperationsClient _documentOperationsClient;

        public CancelDocumentCommandHandler(IDocumentOperationsClient documentOperationsClient)
        {
            _documentOperationsClient = documentOperationsClient;
        }

        public async Task<Result<bool>> Handle(CancelDocumentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _documentOperationsClient.CancelDocumentAsync(request.CompanyId, request.DocumentUUID, request.Reason);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Result.Failure<bool>("CancelDocumentCommand", $"Failed: {errorMessage}");
                }

                return Result.Success(true);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<bool>("CancelDocumentCommand", $"Failed to cancel document: {ex.Message}");
            }
        }
    }
}
