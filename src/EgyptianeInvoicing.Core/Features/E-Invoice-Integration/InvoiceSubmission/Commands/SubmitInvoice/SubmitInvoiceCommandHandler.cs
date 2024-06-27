using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Core.Constants;
using EgyptianeInvoicing.Core.Data.Repositories.Abstractions;
using EgyptianeInvoicing.Core.Features.Authontication.Commands.Authenticate;
using EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetSubmission.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.E_Invoice_Integration.InvoiceSubmission.Commands.SubmitInvoice
{
    public class SubmitInvoiceCommandHandler : ICommandHandler<SubmitInvoiceCommand, SubmissionResponseDto>
    {
        private readonly ILogger<SubmitInvoiceCommandHandler> _logger;
        private readonly ICompanyRepository _companyRepository;
        private readonly IInvoiceSubmissionClient _invoiceSubmissionClient;
        private readonly ITokenSigner _tokenSigner;
        public SubmitInvoiceCommandHandler(ILogger<SubmitInvoiceCommandHandler> logger, ICompanyRepository companyRepository, IInvoiceSubmissionClient invoiceSubmissionClient, ITokenSigner tokenSigner)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _companyRepository = companyRepository;
            _invoiceSubmissionClient = invoiceSubmissionClient;
            _tokenSigner = tokenSigner;
        }

        public async Task<Result<SubmissionResponseDto>> Handle(SubmitInvoiceCommand request, CancellationToken cancellationToken)
        {
            try
            {


                var company = await _companyRepository.GetByIdAsync(request.CompanyId);
                if (company == null)
                    return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"Company with ID '{request.CompanyId}' not found.");

                var companyDto = new CompanyDto
                {
                    Id = company.CommercialRegistrationNo,
                    Name = company.Name,
                    Type = company.Type.ToString(),
                    Address = new AddressDto
                    {
                        
                        Country = "EG",//company.Address.Country,
                        BranchID = company.Address.BranchId.ToString(),
                        PostalCode = company.Address.PostalCode,
                        RegionCity = company.Address.RegionCity,
                        BuildingNumber = company.Address.BuildingNumber,
                        Floor = company.Address.Floor,
                        Governate = company.Address.Governorate,
                        Landmark = company.Address.Landmark,
                        Room = company.Address.Room,
                        Street = company.Address.Street,
                        AdditionalInformation = company.Address.AdditionalInformation,
                    }
                };
                var invoicesDto = new List<DocumentDto>();
                foreach (var invoice in request.Request)
                {
                    var invoiceDto = new DocumentDto();
                    invoiceDto.Issuer = companyDto;
                    #region Customer Information
                    var customerType = CompanyTypes.FromName(invoice.CustomerType);
                    if (customerType == null)
                    {
                        _logger.LogError($"Invalid customer type: {invoice.CustomerType}");
                        return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"Invalid customer type: {invoice.CustomerType}");
                    }

                    var country = CountryCodes.Codes.FirstOrDefault(c => c.ArabicDescription == invoice.Country);
                    if (country == null)
                    {
                        _logger.LogError($"Invalid country: {invoice.Country}");
                        return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"Invalid country: {invoice.Country}");
                    }

                    invoiceDto.Receiver = new CompanyDto
                    {
                        Id = invoice.RegisterNumber,
                        Name = invoice.Name,
                        Type = customerType.Code,
                        Address = new AddressDto
                        {
                            Country = country.Code,
                            Governate = invoice.Governorate,
                            Street = invoice.Street,
                            BuildingNumber = invoice.PropertyNumber,
                            RegionCity = invoice.District
                        }
                    };
                    #endregion
                    var invoiceType = InvoiceType.FromName(invoice.InvoiceType);
                    if (invoiceType == null)
                    {
                        _logger.LogError($"Invalid invoice type: {invoice.InvoiceType}");
                        return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"Invalid invoice type: {invoice.InvoiceType}");
                    }
                    invoiceDto.DocumentType = invoiceType.Code;
                    invoiceDto.DocumentTypeVersion = "1.0";
                    invoiceDto.DateTimeIssued = invoice.IssueDate.ToString("yyyy-MM-ddTHH:mm:ssZ");
                    invoiceDto.TaxpayerActivityCode = company.ActivityCode;
                    invoiceDto.InternalID = invoice.SerialNumber;
                    var invoiceLines = new List<InvoiceLineDto>();
                    double invoiceTotal = 0;
                    foreach (var item in invoice.Items)
                    {
                        var invoiceLine = new InvoiceLineDto();
                        invoiceLine.Description = item.ProductName;
                        invoiceLine.ItemType = item.CodeType;
                        switch (item.CodeType)
                        {
                            case "GS1":
                                invoiceLine.ItemCode = item.InternationalProductCode;
                                break;
                            case "EGS":
                                invoiceLine.ItemCode = item.InternalProductCode;
                                break;
                            default:
                                _logger.LogWarning($"Unknown item code type: {item.CodeType}");
                                break;
                        }
                        var unit = UnitTypes.Codes.FirstOrDefault(u => u.ArabicDescription == item.Unit);
                        if (unit == null)
                        {
                            _logger.LogError($"Invalid unit '{item.Unit}' in invoice serial '{invoice.SerialNumber}'.");
                            return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"Invalid unit '{item.Unit}' in invoice serial '{invoice.SerialNumber}'.");
                        }
                        invoiceLine.UnitType = unit.Code;
                        invoiceLine.Quantity = item.Quantity;
                        invoiceLine.SalesTotal = item.UnitPrice;
                        invoiceLine.ItemsDiscount = item.UnitDiscount;
                        var total = (invoiceLine.SalesTotal * invoiceLine.Quantity) - item.UnitDiscount;
                        invoiceTotal += total;
                        invoiceLine.Total = total;
                        var currency = Currencies.Codes.FirstOrDefault(c => c.ArabicDescription == item.Currency);
                        if (currency == null)
                        {
                            _logger.LogError($"Invalid currency '{item.Currency}' in invoice serial '{invoice.SerialNumber}'.");
                            return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"Invalid currency '{item.Currency}' in invoice serial '{invoice.SerialNumber}'.");
                        }
                        invoiceLine.UnitValue = new UnitValueDto { CurrencySold = currency.Code, AmountEGP = total };

                        var taxableItems = new List<TaxableItemDto>();
                        var vatCode = TaxSubtypes.Codes.FirstOrDefault(c => c.ArabicDescription == item.VATCode);
                        //if (vatCode == null)
                        //{
                        //    _logger.LogError($"Unable to determine VAT code for invoice in serial '{invoice.SerialNumber}'.");
                        //    return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"Unable to determine VAT code for invoice in serial '{invoice.SerialNumber}'.");
                        //}
                        if (vatCode != null)
                        {
                            taxableItems.Add(new TaxableItemDto { TaxType = vatCode.TaxTypeReference, SubType = vatCode.Code, Rate = item.VATPercentage });
                        }
                        var discountTaxCode = TaxSubtypes.Codes.FirstOrDefault(c => c.ArabicDescription == item.DiscountTaxCode);
                        //if (discountTaxCode == null)
                        //{
                        //    _logger.LogError($"Unable to determine discount tax code for invoice in serial '{invoice.SerialNumber}'.");
                        //    return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"Unable to determine discount tax code for invoice in serial '{invoice.SerialNumber}'.");
                        //}
                        if (discountTaxCode != null)
                        {
                            taxableItems.Add(new TaxableItemDto { TaxType = discountTaxCode.TaxTypeReference, SubType = discountTaxCode.Code, Rate = item.DiscountTaxPercentage });
                        }

                        invoiceLines.Add(invoiceLine);
                    }
                    invoiceDto.InvoiceLines = invoiceLines;
                    invoiceDto.TotalAmount = (double)invoiceTotal;
                    invoiceDto.TotalSalesAmount = (double)invoiceTotal;
                    invoiceDto.NetAmount = (double)invoiceTotal;
                    #region Signatures
                    var signatures = new List<SignatureDto>();
                    var signatureValue = _tokenSigner.SignDocuments(SerializeToJson(invoiceDto));
                    if (signatureValue.IsFailure)
                    {
                        _logger.LogError(signatureValue.Error.Message);
                        return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"'{signatureValue.Error.Message}' in invoice serial '{invoice.SerialNumber}'.");
                    }

                    var signature = new SignatureDto { SignatureType = "I", Value = signatureValue.Value };
                    signatures.Add(signature);
                    invoiceDto.Signatures = signatures;
                    #endregion
                    invoicesDto.Add(invoiceDto);
                }
                var response = await _invoiceSubmissionClient.SubmitRegularInvoiceAsync(request.CompanyId, invoicesDto);
                return Result.Success(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            //return null;
            //var response = await _invoiceSubmissionClient.SubmitRegularInvoiceAsync(request.CompanyId, invoicesDto);
        }
        public string SerializeToJson(DocumentDto invoiceDto)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Ensure proper Unicode escaping
            };

            var jsonString = JsonSerializer.Serialize(invoiceDto, options);

            return jsonString;
        }
    }
}
