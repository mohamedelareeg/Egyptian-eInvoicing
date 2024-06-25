using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Features.Companies.Commands.CreateCompany;
using EgyptianeInvoicing.Core.Features.Companies.Commands.DeleteCompany;
using EgyptianeInvoicing.Core.Features.Companies.Commands.UpdateCompany;
using EgyptianeInvoicing.Core.Features.Companies.Queries.GetCompanyById;
using EgyptianeInvoicing.Core.Features.Companies.Queries.SearchCompanies;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Requests;
using EgyptianeInvoicing.SignApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.SignApi.Controllers
{
    [ApiController]
    [Route("api/v1/companies")]
    public class CompaniesController : AppControllerBase
    {
        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(ISender sender, ILogger<CompaniesController> logger)
            : base(sender)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyRequestDto requestDto)
        {
            var command = new CreateCompanyCommand(requestDto);
            var result = await Sender.Send(command);
            return CustomResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var query = new GetCompanyByIdQuery { Id = id };
            var result = await Sender.Send(query);
            return CustomResult(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchCompanies([FromQuery] DataTableOptionsDto options)
        {
            var query = new SearchCompaniesQuery { Options = options };
            var result = await Sender.Send(query);
            return CustomResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyRequestDto requestDto)
        {
            var command = new UpdateCompanyCommand(requestDto);
            var result = await Sender.Send(command);
            return CustomResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var command = new DeleteCompanyCommand { Id = id };
            var result = await Sender.Send(command);
            return CustomResult(result);
        }
    }
}
