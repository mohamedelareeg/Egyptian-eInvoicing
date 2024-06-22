using EgyptianeInvoicing.Shared.Requests;
using EgyptianeInvoicing.MVC.Base;
using EgyptianeInvoicing.MVC.Clients.Abstractions;

namespace EgyptianeInvoicing.MVC.Clients
{
    public class AuthenticationClient : BaseClient, IAuthenticationClient
    {
        public AuthenticationClient(IHttpClientFactory httpClientFactory, ILogger<AuthenticationClient> logger, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory.CreateClient("ApiClient"), logger, httpContextAccessor)
        {
        }

        public async Task<BaseResponse<string>> AuthenticateAsync(AuthenticateRequestDto request)
        {
            return await PostAsync<AuthenticateRequestDto, BaseResponse<string>>("api/v1/authentication/authenticate", request);
        }
    }
}
