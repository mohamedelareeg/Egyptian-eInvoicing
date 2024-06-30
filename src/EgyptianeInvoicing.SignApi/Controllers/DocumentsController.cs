using EgyptianeInvoicing.Core.Features.Authontication.Commands.Authenticate;
using EgyptianeInvoicing.Core.Features.DocumentOperations.Queries.GetRecentDocuments;
using EgyptianeInvoicing.Core.Features.DocumentOperations.Queries.SearchDocuments;
using EgyptianeInvoicing.Core.Features.DocumentPackages.Queries.GetDocumentPackage;
using EgyptianeInvoicing.Core.Features.Documents.Queries.GetDocumentPdf;
using EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice;
using EgyptianeInvoicing.Shared.Dtos;
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
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DocumentsController(ISender sender, ILogger<DocumentsController> logger, IWebHostEnvironment hostingEnvironment)
            : base(sender)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchDocuments([FromBody] SearchDocumentsRequestDto request)
        {
            var result = await Sender.Send(new SearchDocumentsQuery
            {
                CompanyId = request.CompanyId,
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
        [HttpPost("recent")]
        public async Task<IActionResult> RecentDocuments([FromBody] RecentDocumentsRequestDto request)
        {
            var result = await Sender.Send(new GetRecentDocumentsQuery
            {
                CompanyId = request.CompanyId,
                SubmissionDateFrom = request.SubmissionDateFrom,
                SubmissionDateTo = request.SubmissionDateTo,
                IssueDateFrom = request.IssueDateFrom,
                IssueDateTo = request.IssueDateTo,
                PageSize = request.PageSize,
                PageNo = request.PageNo,
                Direction = request.Direction,
                Status = request.Status,
                DocumentType = request.DocumentType,
                ReceiverType = request.ReceiverType,
                ReceiverId = request.ReceiverId,
                IssuerType = request.IssuerType,
                IssuerId = request.IssuerId,
            });
            return CustomResult(result);
        }

        [HttpPost("package/{Rid}")]
        public async Task<IActionResult> GetDocumentPackage(Guid CompanyId, string Rid)
        {
            var result = await Sender.Send(new GetDocumentPackageQuery(CompanyId, Rid));
            return CustomResult(result);
        }
        [HttpPost("pdf/{Rid}")]
        public async Task<IActionResult> GetDocumentPdf(Guid CompanyId, string Rid)
        {
            var result = await Sender.Send(new GetDocumentPdfQuery(CompanyId, Rid));
            return CustomResult(result);
        }
        [HttpPost("submit-invoice")]
        public async Task<IActionResult> SubmitInvoice(SubmitInvoiceRequestDto request)
        {
            var command = new SubmitInvoiceCommand(request.CompanyId, request.Invoices);
            var result = await Sender.Send(command);
            return CustomResult(result);
        }
        [HttpGet("download-import-invoices")]
        public async Task<IActionResult> DownloadImportInvoices()
        {
            try
            {
                var fileName = "import_invoices.xlsx";
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    _logger.LogError($"File {fileName} not found at {filePath}");
                    return NotFound();
                }

                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error downloading {nameof(DownloadImportInvoices)}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        //[HttpGet("download-import-invoices")]
        //public async Task<byte[]> DownloadImportInvoices()
        //{
        //    try
        //    {
        //        var fileName = "import_invoices.xlsx";
        //        var filePath = Path.Combine(_hostingEnvironment.WebRootPath, fileName);

        //        if (!System.IO.File.Exists(filePath))
        //        {
        //            _logger.LogError($"File {fileName} not found at {filePath}");
        //            return null;
        //        }

        //        byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

        //        return fileBytes;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error downloading {nameof(DownloadImportInvoices)}: {ex.Message}");
        //        throw;
        //    }
        //}

    }
}
