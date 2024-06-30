using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Core.Constants;
using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using EgyptianeInvoicing.Core.Features.Authontication.Commands.Authenticate;
using EgyptianeInvoicing.Core.Features.InvoiceSubmission.Commands.SubmitInvoice;
using EgyptianeInvoicing.Core.Models;
using EgyptianeInvoicing.Core.Services;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetSubmission.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Response;
using EgyptianeInvoicing.Shared.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly IInvoiceService _invoiceService;
        public SubmitInvoiceCommandHandler(ILogger<SubmitInvoiceCommandHandler> logger, ICompanyRepository companyRepository, IInvoiceSubmissionClient invoiceSubmissionClient, ITokenSigner tokenSigner, IInvoiceService invoiceService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _companyRepository = companyRepository;
            _invoiceSubmissionClient = invoiceSubmissionClient;
            _tokenSigner = tokenSigner;
            _invoiceService = invoiceService;
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
                var invoicesDto = new List<EInvoiceDto>();
                foreach (var invoice in request.Request)
                {
                    var invoiceDto = new EInvoiceDto();
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
                    double totalSalesAmount = 0, totalDiscountAmount = 0, vatTotal = 0, totalItemsDiscountAmount = 0;

                    foreach (var item in invoice.Items)
                    {
                        var unit = UnitTypes.Codes.FirstOrDefault(u => u.ArabicDescription == item.Unit);
                        if (unit == null)
                        {
                            _logger.LogError($"Invalid unit '{item.Unit}' in invoice serial '{invoice.SerialNumber}'.");
                            return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"Invalid unit '{item.Unit}' in invoice serial '{invoice.SerialNumber}'.");
                        }

                        var invoiceLine = new InvoiceLineDto
                        {
                            Description = item.ProductName,
                            ItemType = item.CodeType,
                            ItemCode = item.CodeType switch
                            {
                                "GS1" => item.InternationalProductCode,
                                "EGS" => item.InternalProductCode,
                                _ => null
                            },
                            UnitType = unit.Code,
                            Quantity = Math.Round(item.Quantity, 5),
                            ItemsDiscount = Math.Round(item.UnitDiscount, 5),
                            Discount = new DiscountDto { Amount = Math.Round(item.UnitDiscount, 5) },
                            SalesTotal = Math.Round(item.UnitPrice * item.Quantity, 5),
                            UnitValue = new UnitValueDto
                            {
                                CurrencySold = Currencies.Codes.FirstOrDefault(c => c.ArabicDescription == item.Currency)?.Code,
                                AmountEGP = Math.Round(item.UnitPrice, 5)
                            }
                        };

                        // Calculate line totals
                        double subtotal = Math.Round((double)((item.UnitPrice * invoiceLine.Quantity) - item.UnitDiscount), 5);
                        double lineTotal = subtotal;

                        // Calculate VAT and other taxes
                        var taxableItems = new List<TaxableItemDto>();
                        var vatCode = TaxSubtypes.Codes.FirstOrDefault(c => c.ArabicDescription == item.VATCode);
                        if (vatCode != null)
                        {
                            double vatValue = Math.Round(subtotal * (item.VATPercentage / 100), 5);
                            vatTotal += vatValue;
                            lineTotal += vatValue;
                            taxableItems.Add(new TaxableItemDto { TaxType = vatCode.TaxTypeReference, SubType = vatCode.Code, Rate = item.VATPercentage, Amount = vatValue });
                        }

                        // Calculate discount taxes
                        var discountTaxCode = TaxSubtypes.Codes.FirstOrDefault(c => c.ArabicDescription == item.DiscountTaxCode);
                        if (discountTaxCode != null)
                        {
                            double discountValue = Math.Round(subtotal * (item.DiscountTaxPercentage / 100), 5);
                            totalDiscountAmount += discountValue;
                            lineTotal -= discountValue;
                            taxableItems.Add(new TaxableItemDto { TaxType = discountTaxCode.TaxTypeReference, SubType = discountTaxCode.Code, Rate = item.DiscountTaxPercentage, Amount = discountValue });
                        }

                        // Summarize totals
                        invoiceLine.Total = Math.Round(lineTotal, 5);
                        invoiceLine.NetTotal = Math.Round(subtotal, 5);
                        invoiceLine.TaxableItems = taxableItems;

                        totalSalesAmount += (double)invoiceLine.SalesTotal;
                        totalItemsDiscountAmount += item.UnitDiscount;

                        invoiceLines.Add(invoiceLine);
                    }


                    // Calculate invoice totals
                    invoiceDto.InvoiceLines = invoiceLines;
                    invoiceDto.TotalSalesAmount = Math.Round(totalSalesAmount, 5);
                    invoiceDto.TotalDiscountAmount = Math.Round(totalItemsDiscountAmount, 5);
                    invoiceDto.NetAmount = Math.Round(totalSalesAmount - totalItemsDiscountAmount, 5);
                    invoiceDto.TotalAmount = Math.Round((double)invoiceDto.NetAmount + vatTotal - totalDiscountAmount, 5);
                    invoiceDto.TaxTotals = new List<TaxTotalDto>
                    {
                        new TaxTotalDto { TaxType = "T1", Amount = Math.Round(vatTotal, 5) },
                        new TaxTotalDto { TaxType = "T4", Amount = Math.Round(totalDiscountAmount, 5) }
                    };
                    invoiceDto.TotalItemsDiscountAmount = Math.Round(totalItemsDiscountAmount, 5);

                    // Sign invoice
                    var signatures = new List<SignatureDto>();
                    var signatureValue = _tokenSigner.SignDocuments(SerializeToJson(invoiceDto), company.Credentials.TokenPin, company.Credentials.Certificate);
                    if (signatureValue.IsFailure)
                    {
                        _logger.LogError(signatureValue.Error.Message);
                        return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"'{signatureValue.Error.Message}' in invoice serial '{invoice.SerialNumber}'.");
                    }
                    signatures.Add(new SignatureDto { SignatureType = "I", Value = signatureValue.Value });
                    invoiceDto.Signatures = signatures;

                    invoicesDto.Add(invoiceDto);
                }

                var response = await _invoiceSubmissionClient.SubmitRegularInvoiceAsync(request.CompanyId, invoicesDto);
                if (response.acceptedDocuments.Count > 0)
                {
                    foreach (var invoice in invoicesDto)
                    {
                        var createdInvoice = await CreateInvoice(invoice, request.CompanyId);
                        if (createdInvoice.IsFailure)
                        {
                            return Result.Failure<SubmissionResponseDto>(createdInvoice.Error);
                        }
                    }
                }
                return Result.Success(response);
            }
            catch (Exception ex)
            {

                throw;
            }
            //return null;
            //var response = await _invoiceSubmissionClient.SubmitRegularInvoiceAsync(request.CompanyId, invoicesDto);
        }
        public string SerializeToJson(EInvoiceDto invoiceDto)
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
        private async Task<Result<Invoice>> CreateInvoice(EInvoiceDto eInvoice, Guid issuerId)
        {
            if (!Enum.TryParse<Shared.Enums.DocumentType>(eInvoice.DocumentType, out var documentType))
            {
                documentType = EnumExtensions.ParseEnumFromDescription<Shared.Enums.DocumentType>(eInvoice.DocumentType);
            }

            var createInvoiceResult = await _invoiceService.CreateInvoiceAsync(
             issuerId,
             Guid.Empty,
             "",
             Guid.Empty,
             Guid.Empty,
             eInvoice.InternalID,
             eInvoice.InvoiceLines,
             eInvoice.Payment,
             eInvoice.Delivery,
             InvoiceStatus.Submitted,
             documentType,
             Shared.Enums.Currency.EGP,
             (double)eInvoice.ExtraDiscountAmount,
             eInvoice.Receiver);

            if (createInvoiceResult.IsFailure)
                return Result.Failure<Invoice>(createInvoiceResult.Error);

            return createInvoiceResult.Value;
        }
    }
}