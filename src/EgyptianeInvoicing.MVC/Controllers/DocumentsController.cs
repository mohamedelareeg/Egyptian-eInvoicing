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

        [HttpPost]
        public async Task<IActionResult> LoadDocuments([FromBody] SearchDocumentsRequestDto parameters)
        {

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
                PublicUrl = doc.publicUrl
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
