namespace EgyptianeInvoicing.Core.Clients.Common.Abstractions
{
    public interface IAuthenticationClient
    {
        Task<string> LoginAndGetAccessTokenAsync(string registrationNumber = null);

    }
}
