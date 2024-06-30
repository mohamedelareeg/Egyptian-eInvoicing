using AutoMapper;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using EgyptianeInvoicing.Core.Data.Abstractions;
using EgyptianeInvoicing.Core.Models;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Core.ValueObjects;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EgyptianeInvoicing.Core.Features.Invoices.Commands.CreateInvoice;
using EgyptianeInvoicing.Shared.Enums;

namespace EgyptianeInvoicing.Core.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyService _companyService;


        public InvoiceService(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork, IMapper mapper, ICompanyRepository companyRepository, ICompanyService companyService)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _companyRepository = companyRepository;
            _companyService = companyService;
        }

        public async Task<Result<Invoice>> CreateInvoiceAsync(
           Guid issuerId,
           Guid receiverId,
           string einvoiceId,
           Guid? purchaseOrderId,
           Guid? salesOrderId,
           string invoiceNumber,
           List<InvoiceLineDto> invoiceLines,
           PaymentDto payment,
           DeliveryDto delivery,
           InvoiceStatus status,
           DocumentType documentType,
           Shared.Enums.Currency currency,
           double extraDiscountAmount,
           CompanyDto receiver = null
       )
        {
            if (!await _companyRepository.ExistsAsync(issuerId))
                return Result.Failure<Invoice>("CreateInvoiceAsync", $"Issuer with ID '{issuerId}' not found.");

            if (receiverId == Guid.Empty || !await _companyRepository.ExistsAsync(receiverId))
            {
                // If receiver doesn't exist and receiverDto is provided, create a new company
                if (receiver != null)
                {
                    var createCompanyResult = await _companyService.CreateCompanyAsync(
                        receiver.Name,
                        "",
                        "",
                        "",
                        receiver.Id,
                        receiver.Address.BranchID,
                        receiver.Address.Country,
                        receiver.Address.Governate,
                        receiver.Address.RegionCity,
                        receiver.Address.Street,
                        receiver.Address.BuildingNumber,
                        receiver.Address.PostalCode,
                        receiver.Address.Floor,
                        receiver.Address.Room,
                        receiver.Address.Landmark,
                        receiver.Address.AdditionalInformation,
                        "",
                        receiver.Type,
                        "",
                        "",
                        "",
                        "",
                        "",
                        null
                    );

                    if (createCompanyResult.IsSuccess)
                    {
                        receiverId = (Guid)createCompanyResult.Value.ReferenceId;
                    }
                    else
                    {
                        return Result.Failure<Invoice>("CreateInvoiceAsync", $"Failed to create receiver company: {createCompanyResult.Error}");
                    }
                }
                else
                {
                    return Result.Failure<Invoice>("CreateInvoiceAsync", $"Receiver with ID '{receiverId}' not found and no receiver data provided.");
                }
            }

            var createdInvoiceResult = Invoice.Create(
                 issuerId,
                 receiverId,
                 einvoiceId,
                 purchaseOrderId,
                 salesOrderId,
                 invoiceNumber,
                 status,
                 documentType,
                 currency);

            if (createdInvoiceResult.IsFailure)
                return Result.Failure<Invoice>(createdInvoiceResult.Error);

            var createdInvoice = createdInvoiceResult.Value;

            foreach (var lineDtoExist in invoiceLines)
            {
                var lineDto = lineDtoExist;
                var createdTaxableItemsResult = CreateTaxableItems(lineDto.TaxableItems);
                if (createdTaxableItemsResult.IsFailure)
                    return Result.Failure<Invoice>(createdTaxableItemsResult.Error);
                lineDto.UnitValue.AmountSold = lineDto.UnitValue.AmountEGP;
                lineDto.UnitValue.CurrencyExchangeRate = 1;
                var createdUnitValueResult = UnitValue.Create(lineDto.UnitValue.CurrencySold, (double)lineDto.UnitValue.AmountSold, (decimal)lineDto.UnitValue.CurrencyExchangeRate);
                if (createdUnitValueResult.IsFailure)
                    return Result.Failure<Invoice>(createdUnitValueResult.Error);
                double discountRate = 0;

                if (lineDto.Discount.Rate == null)
                {
                    if (lineDto.Discount.Amount > 0)
                    {
                        var total = (lineDto.UnitValue.AmountEGP * lineDto.Quantity);
                        discountRate = (double)(lineDto.Discount.Amount / total);
                    }
                }
                else
                {
                    discountRate = (double)lineDto.Discount.Rate;
                }

                var createdDiscountResult = Discount.Create(discountRate, (double)lineDto.Discount.Amount);

                if (createdDiscountResult.IsFailure)
                {
                    return Result.Failure<Invoice>(createdDiscountResult.Error);
                }

                if (!Enum.TryParse(lineDto.UnitType, out UnitType unitType))
                    return Result.Failure<Invoice>("CreateInvoiceCommand", $"Invalid UnitType: {lineDto.UnitType}");

                var createdInvoiceLineResult = InvoiceLine.Create(
                    lineDto.Description,
                    lineDto.ItemCode,
                    unitType,
                    (double)lineDto.Quantity,
                    lineDto.InternalCode,
                    createdUnitValueResult.Value,
                    createdDiscountResult.Value,
                    createdTaxableItemsResult.Value
                );

                if (createdInvoiceLineResult.IsFailure)
                    return Result.Failure<Invoice>(createdInvoiceLineResult.Error);

                createdInvoice.AddInvoiceLine(createdInvoiceLineResult.Value);
            }

            await _invoiceRepository.AddAsync(createdInvoice);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(createdInvoice);
        }

        private Result<List<TaxableItem>> CreateTaxableItems(List<TaxableItemDto> taxableItemDtos)
        {
            var taxableItems = new List<TaxableItem>();

            foreach (var taxableItemDto in taxableItemDtos)
            {
                var createdTaxableItemResult = TaxableItem.Create(taxableItemDto.TaxType, taxableItemDto.SubType, (double)taxableItemDto.Rate);
                if (createdTaxableItemResult.IsFailure)
                    return Result.Failure<List<TaxableItem>>(createdTaxableItemResult.Error);

                taxableItems.Add(createdTaxableItemResult.Value);
            }

            return Result.Success(taxableItems);
        }
    }
}
