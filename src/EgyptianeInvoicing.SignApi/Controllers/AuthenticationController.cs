using EgyptianeInvoicing.Core.Features.Authontication.Commands.Authenticate;
using EgyptianeInvoicing.Shared.Requests;
using EgyptianeInvoicing.SignApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EgyptianeInvoicing.SignApi.Controllers
{
    [ApiController]
    [Route("api/v1/authentication")]
    public class AuthenticationController : AppControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ISender sender, ILogger<AuthenticationController> logger)
            : base(sender)
        {
            _logger = logger;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequestDto request)
        {
            var result = await Sender.Send(new AuthenticateCommand(request.RegistrationNumber));
            return CustomResult(result);
        }
    }
}
