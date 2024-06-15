namespace EgyptianeInvoicing.Core.Clients.Common.Abstractions
{
    public interface IAuthenticationClient
    {
        Task<string> LoginAndGetAccessTokenAsync(string idSrvBaseUrl, string clientId, string clientSecret, string registrationNumber = null);

    }
}
