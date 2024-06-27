using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Commands.UpdateEGSCodeUsage
{
    public class UpdateEGSCodeUsageCommandHandler : ICommandHandler<UpdateEGSCodeUsageCommand, bool>
    {
        private readonly ICodeManagementClient _codeManagementClient;

        public UpdateEGSCodeUsageCommandHandler(ICodeManagementClient codeManagementClient)
        {
            _codeManagementClient = codeManagementClient;
        }

        public async Task<Result<bool>> Handle(UpdateEGSCodeUsageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _codeManagementClient.UpdateEGSCodeUsageAsync(request.CompanyId, request.CodeUsageRequestId, request.Request);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Result.Failure<bool>("UpdateEGSCodeUsageCommand", $"Failed: {errorMessage}");
                }

                return Result.Success(true);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<bool>("UpdateEGSCodeUsageCommand", ex.Message);
            }
        }
    }
}
