using EgyptianeInvoicing.MVC.Base;
using EgyptianeInvoicing.Shared.Requests;

namespace EgyptianeInvoicing.MVC.Clients.Abstractions
{
    public interface IAuthenticationClient
    {
        Task<BaseResponse<string>> AuthenticateAsync(AuthenticateRequestDto request);

    }
}
