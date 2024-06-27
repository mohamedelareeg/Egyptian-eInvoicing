using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using OfficeOpenXml;
using EgyptianeInvoicing.Shared.Dtos;
using Newtonsoft.Json;
using EgyptianeInvoicing.MVC.Clients;
using EgyptianeInvoicing.MVC.Constants;
using EgyptianeInvoicing.MVC.Clients.Abstractions;

namespace EgyptianeInvoicing.MVC.Controllers
{
    public partial class DocumentUploadController : Controller
    {
        private readonly IDocumentsClient _documentsClient;

        public DocumentUploadController(IDocumentsClient documentsClient)
        {
            _documentsClient = documentsClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ImportedInvoiceDto> invoices = null;
            if (TempData["Invoices"] != null)
            {
                invoices = JsonConvert.DeserializeObject<List<ImportedInvoiceDto>>(TempData["Invoices"].ToString());
            }

            return View(invoices);
        }


        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("File", "Please select an Excel file to upload.");
                return BadRequest(ModelState);
            }

            try
            {
                List<ImportedInvoiceDto> invoices = new List<ImportedInvoiceDto>();

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet == null)
                        {
                            ModelState.AddModelError("File", "No worksheet found in the Excel file.");
                            return BadRequest(ModelState);
                        }

                        int startRow = 4;
                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = startRow; row <= rowCount; row++)
                        {
                            string serialNumber = worksheet.Cells[row, 1].GetValue<string>();
                            if (string.IsNullOrEmpty(serialNumber))
                                break;

                            var existingInvoice = invoices.FirstOrDefault(i => i.SerialNumber == serialNumber);

                            if (existingInvoice == null)
                            {
                                try
                                {
                                    var invoice = new ImportedInvoiceDto();
                                    invoice.SerialNumber = serialNumber;
                                    invoice.IssueDate = DateTime.Parse(worksheet.Cells[row, 2].GetValue<string>());
                                    invoice.InvoiceType = worksheet.Cells[row, 3].GetValue<string>();
                                    invoice.AdditionalDiscount = worksheet.Cells[row, 4].GetValue<decimal>();
                                    invoice.Reference = worksheet.Cells[row, 5].GetValue<string>();
                                    invoice.CustomerType = worksheet.Cells[row, 7].GetValue<string>();
                                    invoice.Name = worksheet.Cells[row, 8].GetValue<string>();
                                    invoice.RegisterNumber = worksheet.Cells[row, 9].GetValue<string>();
                                    invoice.Country = worksheet.Cells[row, 10].GetValue<string>();
                                    invoice.Governorate = worksheet.Cells[row, 11].GetValue<string>();
                                    invoice.District = worksheet.Cells[row, 12].GetValue<string>();
                                    invoice.Street = worksheet.Cells[row, 13].GetValue<string>();
                                    invoice.PropertyNumber = worksheet.Cells[row, 14].GetValue<string>();
                                    invoice.Items = new List<ImportedInvoiceItemDto>();

                                    var invoiceItem = new ImportedInvoiceItemDto();
                                    invoiceItem.ProductName = worksheet.Cells[row, 16].GetValue<string>();
                                    invoiceItem.CodeType = worksheet.Cells[row, 17].GetValue<string>();
                                    invoiceItem.InternationalProductCode = worksheet.Cells[row, 18].GetValue<string>();
                                    invoiceItem.InternalProductCode = worksheet.Cells[row, 19].GetValue<string>();
                                    invoiceItem.Unit = worksheet.Cells[row, 20].GetValue<string>();
                                    invoiceItem.Quantity = worksheet.Cells[row, 21].GetValue<double>();
                                    invoiceItem.UnitPrice = worksheet.Cells[row, 22].GetValue<double>();
                                    invoiceItem.UnitDiscount = worksheet.Cells[row, 23].GetValue<double>();
                                    invoiceItem.Currency = worksheet.Cells[row, 24].GetValue<string>();
                                    invoiceItem.CurrencyConvert = worksheet.Cells[row, 25].GetValue<double>();
                                    invoiceItem.VATCode = worksheet.Cells[row, 27].GetValue<string>();
                                    invoiceItem.VATPercentage = worksheet.Cells[row, 28].GetValue<decimal>();
                                    invoiceItem.RelativeTableTaxPercentage = worksheet.Cells[row, 29].GetValue<decimal>();
                                    invoiceItem.SpecificTableTaxPercentage = worksheet.Cells[row, 30].GetValue<decimal>();
                                    invoiceItem.DiscountTaxCode = worksheet.Cells[row, 31].GetValue<string>();
                                    invoiceItem.DiscountTaxPercentage = worksheet.Cells[row, 32].GetValue<decimal>();
                                    invoice.Items.Add(invoiceItem);
                                    invoices.Add(invoice);
                                }
                                catch (Exception ex)
                                {
                                    ModelState.AddModelError("File", $"Error in row {row}: {ex.Message}");
                                    return BadRequest(ModelState);
                                }
                            }
                            else
                            {
                                try
                                {
                                    var invoiceItem = new ImportedInvoiceItemDto();
                                    invoiceItem.ProductName = worksheet.Cells[row, 16].GetValue<string>();
                                    invoiceItem.CodeType = worksheet.Cells[row, 17].GetValue<string>();
                                    invoiceItem.InternationalProductCode = worksheet.Cells[row, 18].GetValue<string>();
                                    invoiceItem.InternalProductCode = worksheet.Cells[row, 19].GetValue<string>();
                                    invoiceItem.Unit = worksheet.Cells[row, 20].GetValue<string>();
                                    invoiceItem.Quantity = worksheet.Cells[row, 21].GetValue<double>();
                                    invoiceItem.UnitPrice = worksheet.Cells[row, 22].GetValue<double>();
                                    invoiceItem.UnitDiscount = worksheet.Cells[row, 23].GetValue<double>();
                                    invoiceItem.Currency = worksheet.Cells[row, 24].GetValue<string>();
                                    invoiceItem.CurrencyConvert = worksheet.Cells[row, 23].GetValue<double>();
                                    invoiceItem.VATCode = worksheet.Cells[row, 27].GetValue<string>();
                                    invoiceItem.VATPercentage = worksheet.Cells[row, 28].GetValue<decimal>();
                                    invoiceItem.RelativeTableTaxPercentage = worksheet.Cells[row, 29].GetValue<decimal>();
                                    invoiceItem.SpecificTableTaxPercentage = worksheet.Cells[row, 30].GetValue<decimal>();
                                    invoiceItem.DiscountTaxCode = worksheet.Cells[row, 31].GetValue<string>();
                                    invoiceItem.DiscountTaxPercentage = worksheet.Cells[row, 32].GetValue<decimal>();
                                    existingInvoice.Items.Add(invoiceItem);
                                }
                                catch (Exception ex)
                                {
                                    ModelState.AddModelError("File", $"Error in row {row}: {ex.Message}");
                                    return BadRequest(ModelState);
                                }
                            }
                        }
                    }
                }
                TempData["SuccessMessage"] = "Excel file imported successfully.";
                TempData["Invoices"] = JsonConvert.SerializeObject(invoices);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("File", $"Error while processing Excel file: {ex.Message}");
                return BadRequest(ModelState);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SubmitInvoice([FromBody] ImportedInvoiceDto invoiceDto)
        {
            try
            {
                var CompanyId = CompanyDtoSingleton.Instance?.ReferenceId;

                var invoices = new List<ImportedInvoiceDto> { invoiceDto };

                var response = await _documentsClient.SubmitInvoiceAsync((Guid)CompanyId, invoices);

                if (response.Succeeded)
                {
                    var submissionResponse = response.Data;
                    return Ok(new { message = "Invoice submitted successfully", submissionResponse });
                }
                else
                {
                    return BadRequest(new { message = "Failed to submit invoice", error = response.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error submitting invoice", error = ex.Message });
            }
        }


        private bool IsUsbConnected()
        {
            return true; 
        }

        private bool IsInvoiceValid(ImportedInvoiceDto invoiceDto)
        {
            return invoiceDto.SerialNumber == "1";
        }


    }
}
