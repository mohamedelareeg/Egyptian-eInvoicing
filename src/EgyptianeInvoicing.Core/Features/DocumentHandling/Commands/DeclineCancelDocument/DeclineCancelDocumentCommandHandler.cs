using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentHandling.Commands.DeclineCancelDocument
{
    public class DeclineCancelDocumentCommandHandler : ICommandHandler<DeclineCancelDocumentCommand, bool>
    {
        private readonly IDocumentHandlingClient _documentHandlingClient;

        public DeclineCancelDocumentCommandHandler(IDocumentHandlingClient documentHandlingClient)
        {
            _documentHandlingClient = documentHandlingClient;
        }

        public async Task<Result<bool>> Handle(DeclineCancelDocumentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _documentHandlingClient.DeclineCancelDocumentAsync(request.DocumentUUID, request.DeclineReason);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Result.Failure<bool>("DeclineCancelDocumentCommand", $"Failed to decline cancellation: {errorMessage}");
                }

                return Result.Success(true);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<bool>("DeclineCancelDocumentCommand", $"Failed to decline cancellation: {ex.Message}");
            }
        }
    }
}
