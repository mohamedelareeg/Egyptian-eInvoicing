using EgyptianeInvoicing.MVC.Clients;
using EgyptianeInvoicing.MVC.Clients.Abstractions;
using EgyptianeInvoicing.MVC.Localization;
using EgyptianeInvoicing.MVC.Middlewares;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GlobalExceptionHandlerMiddleWare>();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("ApiClient", client =>
{
    var apiUrl = builder.Configuration.GetValue<string>("Integrations:Api:BaseUrl");
    client.BaseAddress = new Uri(apiUrl);
});

///////////////////////////////////////////////////////Localizer/////////////////////////////////////////////////
builder.Services.AddLocalization();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(JsonStringLocalizerFactory));
    });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-EG"),
        new CultureInfo("de-DE")
    };

    //options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
////////////////////////////////////////////////////////END//////////////////////////////////////////////////////

builder.Services.AddScoped<IAuthenticationClient, AuthenticationClient>();
builder.Services.AddScoped<IDocumentsClient, DocumentsClient>();
builder.Services.AddScoped<ICompanyClient, CompanyClient>();

builder.Services.AddSession(options =>
{

});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandlerMiddleWare>();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

////////////////////////////////////////////////////////Localizer with Resources//////////////////////////////////
var supportedCultures = new[] { "en-US", "ar-EG", "de-DE" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);
////////////////////////////////////////////////////////END//////////////////////////////////////////////////////

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Company}/{action=Index}/{id?}");

app.Run();