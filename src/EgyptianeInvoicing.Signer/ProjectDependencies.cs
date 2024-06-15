using EgyptianeInvoicing.Signer.Services;
using EgyptianeInvoicing.Signer.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EgyptianeInvoicing.Signer
{
    public static class ProjectDependencies
    {
        public static IServiceCollection AddSignerDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //Services
            services.AddScoped<ISerializationService, JsonSerializationService>();
            services.AddScoped<ISigningService, TokenSigningService>();
            return services;
        }
    }
}
