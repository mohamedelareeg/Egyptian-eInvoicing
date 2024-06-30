using EgyptianeInvoicing.MVC.Clients;
using EgyptianeInvoicing.MVC.Clients.Abstractions;
using EgyptianeInvoicing.MVC.Constants;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Enums;
using EgyptianeInvoicing.Shared.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.MVC.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceClient _invoiceClient;

        public InvoicesController(IInvoiceClient invoiceClient)
        {
            _invoiceClient = invoiceClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoadInvoices(
            [FromQuery] int Draw,
            [FromQuery] int Start,
            [FromQuery] int Length,
            [FromQuery] string SearchText,
            [FromQuery] string SubmissionDateFrom,
            [FromQuery] string SubmissionDateTo,
            [FromQuery] string Status,
            [FromQuery] string DocumentType,
            [FromQuery] string ReceiverType,
            [FromQuery] string ReceiverId,
            [FromQuery] string IssuerType,
            [FromQuery] string IssuerId,
            [FromQuery] string EInvoiceId,
            [FromQuery] string InternalID)
        {
            DateTime? submissionDateFrom = !string.IsNullOrEmpty(SubmissionDateFrom) ? DateTime.Parse(SubmissionDateFrom) : (DateTime?)null;
            DateTime? submissionDateTo = !string.IsNullOrEmpty(SubmissionDateTo) ? DateTime.Parse(SubmissionDateTo) : (DateTime?)null;

            var options = new DataTableOptionsDto
            {
                Draw = Draw,
                Start = Start,
                Length = Length,
                FromDate = submissionDateFrom,
                ToDate = submissionDateTo,
                SearchText = SearchText,
                OrderBy = "",
            };
            var invoiceStatus = !string.IsNullOrEmpty(Status) && Enum.TryParse<InvoiceStatus>(Status, ignoreCase: true, out var parsedInvoiceStatus)
                ? parsedInvoiceStatus
                : (InvoiceStatus?)null;

            var docType = !string.IsNullOrEmpty(DocumentType) && Enum.TryParse<DocumentType>(DocumentType, ignoreCase: true, out var parsedDocumentType)
                ? parsedDocumentType
                : (DocumentType?)null;

            var recType = !string.IsNullOrEmpty(ReceiverType) && Enum.TryParse<CompanyType>(ReceiverType, ignoreCase: true, out var parsedReceiverType)
                ? parsedReceiverType
                : (CompanyType?)null;

            var issType = !string.IsNullOrEmpty(IssuerType) && Enum.TryParse<CompanyType>(IssuerType, ignoreCase: true, out var parsedIssuerType)
                ? parsedIssuerType
                : (CompanyType?)null;

            // Assuming InternalID is also a Guid
            var response = await _invoiceClient.SearchInvoicesAsync(
                options,
                invoiceStatus,
                docType,
                recType,
                Guid.TryParse(ReceiverId, out var recId) ? recId : Guid.Empty,
                issType,
                Guid.TryParse(IssuerId, out var issId) ? issId : Guid.Empty,
                EInvoiceId,
                InternalID
            );

            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }

            var invoices = response.Data;
            var serializedData = invoices.Items.Select((inv, index) => new
            {
                Serial = Start + index + 1,
                SubmissionDate = inv.DateTimeIssued,
                IssueDate = inv.DateTimeIssued,
                DocumentType = inv.DocumentType,
                Receiver = inv.Receiver?.Name,
                Issuer = inv.Issuer?.Name,
                InternalID = inv.InternalID,
                TotalSales = inv.TotalSalesAmount,
                Total = inv.TotalAmount,
                TotalTaxes = inv.TaxTotals?.Sum(t => t.Amount) ?? 0,
            });



            var totalCount = invoices.Count;
            var filteredCount = invoices.Count;

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