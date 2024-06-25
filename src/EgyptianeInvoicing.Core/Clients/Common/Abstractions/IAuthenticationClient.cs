namespace EgyptianeInvoicing.Core.Clients.Common.Abstractions
{
    public interface IAuthenticationClient
    {
        Task<string> LoginAndGetAccessTokenAsync(string clientId, string clientSecret, string registrationNumber = null);

    }
}
