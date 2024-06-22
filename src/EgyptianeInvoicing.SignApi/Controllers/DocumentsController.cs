using EgyptianeInvoicing.Core.Features.Authontication.Commands.Authenticate;
using EgyptianeInvoicing.Core.Features.DocumentOperations.Queries.SearchDocuments;
using EgyptianeInvoicing.Shared.Requests;
using EgyptianeInvoicing.SignApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EgyptianeInvoicing.SignApi.Controllers
{
    [ApiController]
    [Route("api/v1/documents")]
    public class DocumentsController : AppControllerBase
    {
        private readonly ILogger<DocumentsController> _logger;

        public DocumentsController(ISender sender, ILogger<DocumentsController> logger)
            : base(sender)
        {
            _logger = logger;
        }
        [HttpPost("search")]
        public async Task<IActionResult> SearchDocuments([FromBody] SearchDocumentsRequestDto request)
        {
            var result = await Sender.Send(new SearchDocumentsQuery
            {
                SubmissionDateFrom = request.SubmissionDateFrom,
                SubmissionDateTo = request.SubmissionDateTo,
                IssueDateFrom = request.IssueDateFrom,
                IssueDateTo = request.IssueDateTo,
                ContinuationToken = request.ContinuationToken,
                PageSize = request.PageSize,
                Direction = request.Direction,
                Status = request.Status,
                DocumentType = request.DocumentType,
                ReceiverType = request.ReceiverType,
                ReceiverId = request.ReceiverId,
                IssuerType = request.IssuerType,
                IssuerId = request.IssuerId,
                UUID = request.UUID,
                InternalID = request.InternalID
            });
            return CustomResult(result);
        }
    }
}
