using EgyptianeInvoicing.MVC.Clients.Abstractions;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EgyptianeInvoicing.MVC.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyClient _companyClient;

        public CompanyController(ICompanyClient companyClient)
        {
            _companyClient = companyClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadData(DataTableOptionsDto parameters)
        {
            var result = await _companyClient.SearchCompaniesAsync(parameters);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var companies = result.Data.Items.Select(c => new
            {
                CommercialRegistrationNumber = c.Id,
                Name = c.Name,
                Type = c.Type,
                ReferenceId = c.ReferenceId
            });

            return Json(new
            {
                draw = parameters.Draw,
                recordsTotal = result.Data.Count,
                recordsFiltered = result.Data.Count,
                data = companies
            });
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _companyClient.GetCompanyAsync(id);
            if (result.Succeeded)
            {
                return View(result.Data);
            }
            else
            {
                // Handle failure
                ViewBag.Error = result.Errors;
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCompanyRequestDto requestDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _companyClient.CreateCompanyAsync(requestDto);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle failure
                    ViewBag.Error = result.Errors;
                    return View(requestDto);
                }
            }
            return View(requestDto);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _companyClient.GetCompanyAsync(id);
            if (result.Succeeded)
            {
                return View(result.Data);
            }
            else
            {
                // Handle failure
                ViewBag.Error = result.Errors;
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _companyClient.DeleteCompanyAsync(id);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle failure
                ViewBag.Error = result.Errors;
                return View("Error");
            }
        }
    }
}
