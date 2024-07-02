using System;
using System.Configuration;
using System.Data;
using System.Windows;
using EgyptianeInvoicing.Signer;
using EgyptianeInvoicing.Signer.Services.Abstractions;
using EgyptianeInvoicing.Signer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using Serilog;
using Serilog.Formatting.Compact;

namespace EgyptianeInvoicing.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {

        public static IServiceProvider ServiceProvider { get; private set; }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var services = new ServiceCollection();

            //services.AddSignerDependencies();
            Serilog.Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Information() // Set minimum log level to Information
                  .WriteTo.File(new RenderedCompactJsonFormatter(), $"Logs/log-{DateTime.Now:yyyy-MM-dd}.txt")
                  .CreateLogger();


            // Add logging services
            services.AddLogging(loggingBuilder =>
            {
                // Add Serilog
                loggingBuilder.AddSerilog();
            });
            services.AddScoped<ISerializationService, JsonSerializationService>();
            services.AddScoped<ISigningService, TokenSigningService>();
            services.AddScoped<ITokenSigner, TokenSigner>();
            services.AddTransient(typeof(HomeWindow));
            ServiceProvider = services.BuildServiceProvider();
            var window = ServiceProvider.GetRequiredService<HomeWindow>();
            window.Show();

        }
        
    }
}
