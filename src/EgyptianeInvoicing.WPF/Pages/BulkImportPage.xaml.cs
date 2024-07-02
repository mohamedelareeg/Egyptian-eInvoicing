using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using EgyptianeInvoicing.Shared.Dtos;
using System.IO;
using OfficeOpenXml;
using System.Text;
using EgyptianeInvoicing.Signer.Services.Abstractions;
using EgyptianeInvoicing.Signer;
using Org.BouncyCastle.Crypto.Tls;

namespace EgyptianeInvoicing.WPF.Pages
{
    public partial class BulkImportPage : UserControl
    {
        public ObservableCollection<ImportedInvoiceDto> Invoices { get; set; }
        private readonly ITokenSigner _tokenSigner;

        public BulkImportPage(ITokenSigner tokenSigner)
        {
            InitializeComponent();
            DataContext = this;
            Invoices = new ObservableCollection<ImportedInvoiceDto>();
            _tokenSigner = tokenSigner;
        }

        private async void DownloadSampleFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync("https://localhost:7253/api/v1/documents/download-import-invoices");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsByteArrayAsync();

                        // Show save file dialog
                        var saveFileDialog = new SaveFileDialog();
                        saveFileDialog.FileName = "sample_file.xlsx"; 
                        saveFileDialog.DefaultExt = ".xlsx"; 
                        saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*"; 

                        // Show save file dialog and process result
                        if (saveFileDialog.ShowDialog() == true)
                        {
                            // Get selected file name and path
                            var filePath = saveFileDialog.FileName;

                            // Write content to the file
                            File.WriteAllBytes(filePath, content);

                            MessageBox.Show("Sample file downloaded and saved successfully.");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Failed to download sample file. Status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading or saving sample file: {ex.Message}");
            }
        }

        private void BrowseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private async void ImportInvoices_Click(object sender, RoutedEventArgs e)
        {
            var filePath = FilePathTextBox.Text;
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("Please select a valid Excel file to upload.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Invoices.Clear();

                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet == null)
                        {
                            MessageBox.Show("No worksheet found in the Excel file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        int startRow = 4;
                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = startRow; row <= rowCount; row++)
                        {
                            string serialNumber = worksheet.Cells[row, 1].GetValue<string>();
                            if (string.IsNullOrEmpty(serialNumber))
                                break;

                            var existingInvoice = Invoices.FirstOrDefault(i => i.SerialNumber == serialNumber);

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
                                    invoiceItem.VATPercentage = worksheet.Cells[row, 28].GetValue<double>();
                                    invoiceItem.RelativeTableTaxPercentage = worksheet.Cells[row, 29].GetValue<double>();
                                    invoiceItem.SpecificTableTaxPercentage = worksheet.Cells[row, 30].GetValue<double>();
                                    invoiceItem.DiscountTaxCode = worksheet.Cells[row, 31].GetValue<string>();
                                    invoiceItem.DiscountTaxPercentage = worksheet.Cells[row, 32].GetValue<double>();
                                    invoice.Items.Add(invoiceItem);
                                    Invoices.Add(invoice);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error in row {row}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
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
                                    invoiceItem.CurrencyConvert = worksheet.Cells[row, 25].GetValue<double>();
                                    invoiceItem.VATCode = worksheet.Cells[row, 27].GetValue<string>();
                                    invoiceItem.VATPercentage = worksheet.Cells[row, 28].GetValue<double>();
                                    invoiceItem.RelativeTableTaxPercentage = worksheet.Cells[row, 29].GetValue<double>();
                                    invoiceItem.SpecificTableTaxPercentage = worksheet.Cells[row, 30].GetValue<double>();
                                    invoiceItem.DiscountTaxCode = worksheet.Cells[row, 31].GetValue<string>();
                                    invoiceItem.DiscountTaxPercentage = worksheet.Cells[row, 32].GetValue<double>();
                                    existingInvoice.Items.Add(invoiceItem);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error in row {row}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                        }
                    }
                }

                var allErrors = new Dictionary<string, List<string>>();

                foreach (var invoice in Invoices)
                {
                    var validationErrors = IsValidInvoice(invoice);

                    if (validationErrors.Count > 0)
                    {
                        Invoices.Clear();
                        allErrors[$"{invoice.SerialNumber}"] = validationErrors;
                    }

                    foreach (var item in invoice.Items)
                    {
                        var productValidationErrors = IsValidInvoiceProduct(item);
                        if (productValidationErrors.Count > 0)
                        {
                            Invoices.Clear();
                            if (allErrors.ContainsKey($"# {invoice.SerialNumber}"))
                            {
                                allErrors[$"{invoice.SerialNumber}"].AddRange(productValidationErrors);
                            }
                            else
                            {
                                allErrors[$"{invoice.SerialNumber}"] = productValidationErrors;
                            }
                        }
                    }
                }

                if (allErrors.Count > 0)
                {
                    string errorMessage = "";

                    foreach (var errorEntry in allErrors)
                    {
                        errorMessage += $"#{errorEntry.Key}:\n";
                        foreach (var error in errorEntry.Value)
                        {
                            errorMessage += $"- {error}\n";
                        }
                        errorMessage += "\n"; // Add a blank line between invoices for clarity
                    }

                    MessageBox.Show(errorMessage, "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                MessageBox.Show("Excel file imported successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while processing Excel file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ViewItems_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to view invoice items here
        }

        private async void SubmitInvoices_Click(object sender, RoutedEventArgs e)
        {
            if (Invoices == null || Invoices.Count == 0) return;

            if (MessageBox.Show("Are you sure you want to submit these invoices? Make sure the USB signature token is connected.", "Submit Invoices", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                int totalInvoices = Invoices.Count;
                int currentInvoice = 0;

                foreach (var invoice in Invoices)
                {
                    var signedToken = _tokenSigner.SignDocuments("05827500", "MCDR CA 2018");

                    if (signedToken.Value == null)
                    {
                        MessageBox.Show("Error signing with the USB token. Please check the USB token connection and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    // Call your API to submit each invoice here
                    var success = await SubmitInvoiceAsync("5c421136-056b-4345-bd4a-a57053c96937", invoice, signedToken.Value);

                    currentInvoice++;
                    SubmissionProgressBar.Value = ((double)currentInvoice / totalInvoices) * 100;
                    SubmissionStatusTextBlock.Text = $"Submitting invoice {currentInvoice} of {totalInvoices}";

                    if (!success)
                    {
                        // Handle error case
                        MessageBox.Show($"Failed to submit invoice {invoice.SerialNumber}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                MessageBox.Show("All invoices have been submitted successfully.", "Completed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private async Task<bool> SubmitInvoiceAsync(string CompanyId, ImportedInvoiceDto invoice , SigningResultDto signingResultDto)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Prepare the request DTO with CompanyId and invoice
                    var requestDto = new
                    {
                        CompanyId = CompanyId,
                        //certificate = signingResultDto.CertificateHandle,
                        //certForSigning = signingResultDto.CertificateForSigning,
                        Invoices = new List<ImportedInvoiceDto> { invoice }
                        
                    };

                    var json = JsonConvert.SerializeObject(requestDto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("https://localhost:7253/api/v1/documents/submit-invoice", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Invoice submitted successfully.");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Failed to submit invoice. Status code: {response.StatusCode}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting invoice: {ex.Message}");
                return false;
            }
        }


        public List<string> IsValidInvoice(ImportedInvoiceDto invoice)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(invoice.SerialNumber))
            {
                errors.Add("Serial number is required.");
            }

            if (invoice.IssueDate.Date != DateTime.Today)
            {
                errors.Add($"Issue date must be today's date: {DateTime.Today.ToShortDateString()}");
            }

            List<string> validInvoiceTypes = new List<string> { "فاتورة", "اشعار دائن", "اشعار مدين" };
            if (!validInvoiceTypes.Contains(invoice.InvoiceType))
            {
                errors.Add("Invalid invoice type.");
            }

            List<string> validCustomerTypes = new List<string> { "شركة", "أجنبي", "فرد" };
            if (!validCustomerTypes.Contains(invoice.CustomerType))
            {
                errors.Add("Invalid customer type.");
            }

            if (string.IsNullOrEmpty(invoice.Name))
            {
                errors.Add("Name is required.");
            }

            if (invoice.CustomerType == "Company")
            {
                if (string.IsNullOrEmpty(invoice.RegisterNumber))
                {
                    errors.Add("Register number is required for companies.");
                }
                else if (invoice.RegisterNumber.Length != 9)
                {
                    errors.Add("Register number must be 9 characters long.");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(invoice.RegisterNumber))
                {
                    errors.Add("Register number is required.");
                }
            }

            if (string.IsNullOrEmpty(invoice.Country))
            {
                errors.Add("Country is required.");
            }

            if (string.IsNullOrEmpty(invoice.Governorate))
            {
                errors.Add("Governorate is required.");
            }

            if (string.IsNullOrEmpty(invoice.Street))
            {
                errors.Add("Street is required.");
            }

            return errors;
        }


        public List<string> IsValidInvoiceProduct(ImportedInvoiceItemDto item)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(item.ProductName))
            {
                errors.Add("Product name is required.");
            }

            if (item.CodeType != "GS1" && item.CodeType != "EGs")
            {
                errors.Add("Invalid code type.");
            }

            if (item.CodeType == "GS1" && string.IsNullOrEmpty(item.InternationalProductCode))
            {
                errors.Add("International product code is required for GS1 code type.");
            }

            if (item.CodeType == "EGs" && string.IsNullOrEmpty(item.InternalProductCode))
            {
                errors.Add("Internal product code is required for EGs code type.");
            }

            if (string.IsNullOrEmpty(item.Unit))
            {
                errors.Add("Unit is required.");
            }

            if (item.Quantity <= 0)
            {
                errors.Add("Quantity must be greater than zero.");
            }

            if (item.UnitPrice <= 0)
            {
                errors.Add("Unit price must be greater than zero.");
            }

            if (string.IsNullOrEmpty(item.Currency))
            {
                errors.Add("Currency is required.");
            }

            if (item.Currency != "جنيه مصري" && (item.CurrencyConvert == null || item.CurrencyConvert <= 0))
            {
                errors.Add("Currency convert is required for non-Egyptian Pound currencies.");
            }

            return errors;
        }

    }
}
