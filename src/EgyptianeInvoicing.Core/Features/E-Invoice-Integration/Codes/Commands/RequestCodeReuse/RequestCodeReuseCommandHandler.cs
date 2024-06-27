using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using MediatR;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Commands.RequestCodeReuse
{
    public class RequestCodeReuseCommandHandler : ICommandHandler<RequestCodeReuseCommand, bool>
    {
        private readonly ICodeManagementClient _codeManagementClient;

        public RequestCodeReuseCommandHandler(ICodeManagementClient codeManagementClient)
        {
            _codeManagementClient = codeManagementClient;
        }

        public async Task<Result<bool>> Handle(RequestCodeReuseCommand request, CancellationToken cancellationToken)
        {
            var response = await _codeManagementClient.RequestCodeReuseAsync(request.CompanyId, request.Request);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return Result.Failure<bool>("RequestCodeReuseCommand", $"Failed: {errorMessage}");
            }

            return Result.Success(true);
        }
    }
}
