using BuildingBlocks.Behaviors;
using EgyptianeInvoicing.Core.Clients.Common;
using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using EgyptianeInvoicing.Core.Clients.Invoicing;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Core.Services;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.SignerDto;
using EgyptianeInvoicing.Signer.Services;
using EgyptianeInvoicing.Signer.Services.Abstractions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using EgyptianeInvoicing.Signer;
using EgyptianeInvoicing.Core.Data;
using Microsoft.EntityFrameworkCore;
using EgyptianeInvoicing.Core.Features.Companies;
using EgyptianeInvoicing.Core.Data.Repositories.Abstractions;
using EgyptianeInvoicing.Core.Data.Repositories;

namespace EgyptianeInvoicing.Core
{
    public static class ProjectDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services, IConfiguration _configuration)
        {
            #region Mediator & Pipelines
            //MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                //MediatR PopleLines
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationPiplineBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
            });

            //Fluent Validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
            #endregion
            #region AutoMapper
            // Mapping Profiles
            services.AddAutoMapper(typeof(CompanyProfile));
            #endregion

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("signersettings.json", optional: false, reloadOnChange: true)
                .Build();

            services.Configure<TokenSigningSettingsDto>(configuration.GetSection("TokenSigningSettings"));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                string connectionString = configuration.GetConnectionString("SqlServer");
                options.UseSqlServer(connectionString);
            });
            #region E-Invoice Environment
            var env = configuration.GetSection("E-InvoiceEnvironment").Value;

            var idSrvBaseUrl = configuration.GetSection($"E-InvoiceEnvironments:{env}:IdSrvBaseUrl").Value;
            var invoicingBaseUrl = configuration.GetSection($"E-InvoiceEnvironments:{env}:InvoicingBaseUrl").Value;
            var systemApiBaseUrl = configuration.GetSection($"E-InvoiceEnvironments:{env}:SystemApiBaseUrl").Value;
            var registrationPortalBaseUrl = configuration.GetSection($"E-InvoiceEnvironments:{env}:RegistrationPortalBaseUrl").Value;

            services.AddHttpClient("IdSrvBaseUrl", client =>
            {
                client.BaseAddress = new Uri(idSrvBaseUrl);
            });

            services.AddHttpClient("InvoicingBaseUrl", client =>
            {
                client.BaseAddress = new Uri(invoicingBaseUrl);
            });

            services.AddHttpClient("SystemApiBaseUrl", client =>
            {
                client.BaseAddress = new Uri(systemApiBaseUrl);
            });

            services.AddHttpClient("RegistrationPortalBaseUrl", client =>
            {
                client.BaseAddress = new Uri(registrationPortalBaseUrl);
            });
            #endregion

            //Clients
            #region Common
            services.AddScoped<IAuthenticationClient, AuthenticationClient>();
            services.AddScoped<ICodeManagementClient, CodeManagementClient>();
            services.AddScoped<IDocumentTypesClient, DocumentTypesClient>();
            services.AddScoped<INotificationsClient, NotificationsClient>();
            #endregion
            #region Invoicing
            services.AddScoped<IDocumentHandlingClient, DocumentHandlingClient>();
            services.AddScoped<IDocumentNotificationClient, DocumentNotificationClient>();
            services.AddScoped<IDocumentOperationsClient, DocumentOperationsClient>();
            services.AddScoped<IDocumentPackageClient, DocumentPackageClient>();
            services.AddScoped<IDocumentRetrievalClient, DocumentRetrievalClient>();
            services.AddScoped<IInvoiceSubmissionClient, InvoiceSubmissionClient>();
            #endregion
            //Services
            services.AddSignerDependencies(_configuration);
            services.AddScoped<ISecureStorageService, SecureStorageService>();
            services.AddScoped<ITokenSigner, TokenSigner>();
            //Repositories
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            return services;
        }
    }
}
