using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using EgyptianeInvoicing.Core.Services.Abstractions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Commands.CreateEGSCodeUsage
{
    public class CreateEGSCodeUsageCommandHandler : ICommandHandler<CreateEGSCodeUsageCommand, bool>
    {
        private readonly ICodeManagementClient _codeManagementClient;

        public CreateEGSCodeUsageCommandHandler(ICodeManagementClient codeManagementClient)
        {
            _codeManagementClient = codeManagementClient;
        }

        public async Task<Result<bool>> Handle(CreateEGSCodeUsageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _codeManagementClient.CreateEGSCodeUsageAsync(request.Request);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Result.Failure<bool>("CreateEGSCodeUsageCommand", $"Failed: {errorMessage}");
                }

                return Result.Success(true);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<bool>("CreateEGSCodeUsageCommand", ex.Message);
            }
        }
    }
}
