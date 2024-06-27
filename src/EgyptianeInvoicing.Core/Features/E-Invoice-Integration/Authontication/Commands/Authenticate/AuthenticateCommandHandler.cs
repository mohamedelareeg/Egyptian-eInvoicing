using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using EgyptianeInvoicing.Core.Data.Repositories.Abstractions;
using EgyptianeInvoicing.Core.Features.Authontication.Commands.Authenticate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Authentication.Commands.Authenticate
{
    public class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, string>
    {
        private readonly IAuthenticationClient _authenticationClient;
        private readonly ICompanyRepository _companyRepository;

        public AuthenticateCommandHandler(IAuthenticationClient authenticationClient, ICompanyRepository companyRepository)
        {
            _authenticationClient = authenticationClient;
            _companyRepository = companyRepository;
        }

        public async Task<Result<string>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var company = await _companyRepository.GetByIdAsync(request.CompanyId);
                if (company == null)
                    return Result.Failure<string>("AuthenticateCommandHandler.Handle", $"Company with ID '{request.CompanyId}' not found.");

                var credentials = company.Credentials;
                if (credentials == null)
                    return Result.Failure<string>("AuthenticateCommandHandler.Handle", $"Company credentials are not set.");

                if (string.IsNullOrEmpty(credentials.ClientId) || string.IsNullOrEmpty(credentials.ClientSecret1))
                    return Result.Failure<string>("AuthenticateCommandHandler.Handle", $"Invalid company credentials.");

                var token = await _authenticationClient.LoginAndGetAccessTokenAsync(request.CompanyId, credentials.ClientId, credentials.ClientSecret1, null);// company.CommercialRegistrationNo);
                return Result.Success(token);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(new Error("AuthenticateCommandHandler.Handle", ex.Message));
            }
        }
    }
}
