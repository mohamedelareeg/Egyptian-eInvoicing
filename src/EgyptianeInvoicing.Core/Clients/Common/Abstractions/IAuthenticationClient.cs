namespace EgyptianeInvoicing.Core.Clients.Common.Abstractions
{
    public interface IAuthenticationClient
    {
        Task<string> LoginAndGetAccessTokenAsync(Guid companyId, string clientId, string clientSecret, string registrationNumber = null);

    }
}
