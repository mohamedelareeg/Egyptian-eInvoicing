using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentHandling.Commands.DeclineRejection
{
    public class DeclineRejectionCommandHandler : ICommandHandler<DeclineRejectionCommand, bool>
    {
        private readonly IDocumentHandlingClient _documentHandlingClient;

        public DeclineRejectionCommandHandler(IDocumentHandlingClient documentHandlingClient)
        {
            _documentHandlingClient = documentHandlingClient;
        }

        public async Task<Result<bool>> Handle(DeclineRejectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _documentHandlingClient.DeclineRejectionAsync(request.CompanyId, request.DocumentUUID);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Result.Failure<bool>("DeclineRejectionCommand", $"Failed to decline rejection: {errorMessage}");
                }

                return Result.Success(true);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<bool>("DeclineRejectionCommand", $"Failed to decline rejection: {ex.Message}");
            }
        }
    }
}
