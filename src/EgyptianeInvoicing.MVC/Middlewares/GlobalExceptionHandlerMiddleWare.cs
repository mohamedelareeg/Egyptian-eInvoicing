
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace EgyptianeInvoicing.MVC.Middlewares
{
    internal class GlobalExceptionHandlerMiddleWare : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleWare> _logger;
        public GlobalExceptionHandlerMiddleWare(ILogger<GlobalExceptionHandlerMiddleWare> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message);

            }
        }
    }
}
