using EgyptianeInvoicing.MVC.Clients.Abstractions;
using EgyptianeInvoicing.MVC.Models;
using EgyptianeInvoicing.Shared.Requests;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EgyptianeInvoicing.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthenticationClient _authenticationClient;

        public HomeController(ILogger<HomeController> logger, IAuthenticationClient authenticationClient)
        {
            _logger = logger;
            _authenticationClient = authenticationClient;
        }
        [HttpPost]
        public IActionResult setLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthenticateRequestDto request)
        {
            var response = await _authenticationClient.AuthenticateAsync(request);

            if (response.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
