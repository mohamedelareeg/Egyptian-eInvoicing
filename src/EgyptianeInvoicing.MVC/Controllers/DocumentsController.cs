using EgyptianeInvoicing.MVC.Clients;
using EgyptianeInvoicing.MVC.Clients.Abstractions;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EgyptianeInvoicing.MVC.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IDocumentsClient _documentsClient;

        public DocumentsController(IDocumentsClient documentsClient)
        {
            _documentsClient = documentsClient;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchDocument()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoadSearchDocuments(
     [FromQuery] int Draw,
     [FromQuery] int Start,
     [FromQuery] int Length,
     [FromQuery] string SearchText,
     [FromQuery] string SubmissionDateFrom,
     [FromQuery] string SubmissionDateTo,
     [FromQuery] string IssueDateFrom,
     [FromQuery] string IssueDateTo,
     [FromQuery] string Status,
     [FromQuery] string DocumentType,
     [FromQuery] string ReceiverType,
     [FromQuery] string ReceiverId,
     [FromQuery] string IssuerType,
     [FromQuery] string IssuerId,
     [FromQuery] string UUID,
     [FromQuery] string InternalID)
        {
            DateTime? submissionDateFrom = !string.IsNullOrEmpty(SubmissionDateFrom) ? DateTime.Parse(SubmissionDateFrom) : (DateTime?)null;
            DateTime? submissionDateTo = !string.IsNullOrEmpty(SubmissionDateTo) ? DateTime.Parse(SubmissionDateTo) : (DateTime?)null;
            DateTime? issueDateFrom = !string.IsNullOrEmpty(IssueDateFrom) ? DateTime.Parse(IssueDateFrom) : (DateTime?)null;
            DateTime? issueDateTo = !string.IsNullOrEmpty(IssueDateTo) ? DateTime.Parse(IssueDateTo) : (DateTime?)null;

            var parameters = new SearchDocumentsRequestDto
            {
                PageSize = Length,
                SubmissionDateFrom = submissionDateFrom,
                SubmissionDateTo = submissionDateTo,
                IssueDateFrom = issueDateFrom,
                IssueDateTo = issueDateTo,
                Status = Status,
                DocumentType = DocumentType,
                ReceiverType = ReceiverType,
                ReceiverId = ReceiverId,
                IssuerType = IssuerType,
                IssuerId = IssuerId,
                UUID = UUID,
                InternalID = InternalID
            };

            var response = await _documentsClient.SearchDocumentsAsync(parameters);

            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }

            var documents = response.Data;
            var serializedData = documents.result.Select((doc, index) => new
            {
                Serial = index + 1,
                SubmissionDate = doc.dateTimeReceived?.ToString("yyyy-MM-dd"),
                IssueDate = doc.dateTimeIssued?.ToString("yyyy-MM-dd"),
                Status = doc.status,
                DocumentType = doc.typeName,
                Receiver = doc.receiverName,
                Issuer = doc.issuerName,
                UUID = doc.uuid,
                InternalID = doc.internalId,
                PublicUrl = doc.publicUrl,
                TotalSales = doc.totalSales,
            });
            var totalCount = documents.metadata.totalCount ?? documents.result.Count;
            var filteredCount = documents.metadata.totalCount ?? documents.result.Count;

            var responseDatatable = new
            {
                recordsTotal = totalCount,
                recordsFiltered = filteredCount,
                data = serializedData
            };
            return Json(responseDatatable);
        }

        [HttpGet]
        public IActionResult RecentDocument()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoadRecentDocuments(
     [FromQuery] int Draw,
     [FromQuery] int Start,
     [FromQuery] int Length,
     [FromQuery] string SearchText,
     [FromQuery] string SubmissionDateFrom,
     [FromQuery] string SubmissionDateTo,
     [FromQuery] string IssueDateFrom,
     [FromQuery] string IssueDateTo,
     [FromQuery] string Status,
     [FromQuery] string DocumentType,
     [FromQuery] string ReceiverType,
     [FromQuery] string ReceiverId,
     [FromQuery] string IssuerType,
     [FromQuery] string IssuerId)
        {
            DateTime? submissionDateFrom = !string.IsNullOrEmpty(SubmissionDateFrom) ? DateTime.Parse(SubmissionDateFrom) : (DateTime?)null;
            DateTime? submissionDateTo = !string.IsNullOrEmpty(SubmissionDateTo) ? DateTime.Parse(SubmissionDateTo) : (DateTime?)null;
            DateTime? issueDateFrom = !string.IsNullOrEmpty(IssueDateFrom) ? DateTime.Parse(IssueDateFrom) : (DateTime?)null;
            DateTime? issueDateTo = !string.IsNullOrEmpty(IssueDateTo) ? DateTime.Parse(IssueDateTo) : (DateTime?)null;
            int pageNo = Start / Length + 1;
            var parameters = new RecentDocumentsRequestDto
            {
                PageSize = Length,
                PageNo = pageNo,
                SubmissionDateFrom = submissionDateFrom,
                SubmissionDateTo = submissionDateTo,
                IssueDateFrom = issueDateFrom,
                IssueDateTo = issueDateTo,
                Status = Status,
                DocumentType = DocumentType,
                ReceiverType = ReceiverType,
                ReceiverId = ReceiverId,
                IssuerType = IssuerType,
                IssuerId = IssuerId,
            };

            var response = await _documentsClient.RecentDocumentsAsync(parameters);

            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }

            var documents = response.Data;
            var serializedData = documents.result.Select((doc, index) => new
            {
                Serial = Start + index + 1,
                SubmissionDate = doc.dateTimeReceived?.ToString("yyyy-MM-dd"),
                IssueDate = doc.dateTimeIssued?.ToString("yyyy-MM-dd"),
                Status = doc.status,
                DocumentType = doc.typeName,
                Receiver = doc.receiverName,
                Issuer = doc.issuerName,
                UUID = doc.uuid,
                InternalID = doc.internalId,
                PublicUrl = doc.publicUrl,
                TotalSales = doc.totalSales,
            });
            var totalCount = documents.metadata.totalCount ?? documents.result.Count;
            var filteredCount = documents.metadata.totalCount ?? documents.result.Count;

            var responseDatatable = new
            {
                recordsTotal = totalCount,
                recordsFiltered = filteredCount,
                data = serializedData
            };
            return Json(responseDatatable);
        }
    }
  
}

