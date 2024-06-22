using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.UpdateCode.Request;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Commands.UpdateCode
{
    public class UpdateCodeCommandHandler : IRequestHandler<UpdateCodeCommand, Result<bool>>
    {
        private readonly ICodeManagementClient _codeManagementClient;

        public UpdateCodeCommandHandler(ICodeManagementClient codeManagementClient)
        {
            _codeManagementClient = codeManagementClient;
        }

        public async Task<Result<bool>> Handle(UpdateCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _codeManagementClient.UpdateCodeAsync(request.CodeType, request.ItemCode, request.Request);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Result.Failure<bool>("UpdateCodeCommand", $"Failed: {errorMessage}");
                }

                return Result.Success(true);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<bool>("UpdateCodeCommand:",ex.Message);
            }
        }
    }
}
