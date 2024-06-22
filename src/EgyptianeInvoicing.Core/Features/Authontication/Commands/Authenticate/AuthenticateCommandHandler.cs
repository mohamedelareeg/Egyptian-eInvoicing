using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Authontication.Commands.Authenticate
{
    public class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, string>
    {
        private readonly IAuthenticationClient _authenticationClient;

        public AuthenticateCommandHandler(IAuthenticationClient authenticationClient)
        {
            _authenticationClient = authenticationClient;
        }

        public async Task<Result<string>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _authenticationClient.LoginAndGetAccessTokenAsync(request.RegistrationNumber);
                return Result.Success(token);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<string>(new Error("AuthenticateCommandHandler.Handle", ex.Message));
            }
        }
    }
}
