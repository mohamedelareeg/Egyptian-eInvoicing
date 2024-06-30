using AutoMapper;
using EgyptianeInvoicing.Core.Models;
using EgyptianeInvoicing.Core.ValueObjects;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using System;

namespace EgyptianeInvoicing.Core.Features.Invoices
{
    public class InvoicesProfile : Profile
    {
        public InvoicesProfile()
        {
            CreateMap<Invoice, InvoiceDto>()
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType.ToString()))
                .ForMember(dest => dest.DocumentTypeVersion, opt => opt.MapFrom(src => "1.0"))
                .ForMember(dest => dest.DateTimeIssued, opt => opt.MapFrom(src => src.CreatedOnUtc.ToString("yyyy-MM-ddTHH:mm:ssZ")))
                .ForMember(dest => dest.InternalID, opt => opt.MapFrom(src => src.InvoiceNumber))
                .ForMember(dest => dest.PurchaseOrderReference, opt => opt.MapFrom(src => src.PurchaseOrderId.ToString()))
                .ForMember(dest => dest.PurchaseOrderDescription, opt => opt.MapFrom(src => "Purchase order description"))
                .ForMember(dest => dest.SalesOrderReference, opt => opt.MapFrom(src => src.SalesOrderId.ToString()))
                .ForMember(dest => dest.SalesOrderDescription, opt => opt.MapFrom(src => "Sales order description"))
                .ForMember(dest => dest.ProformaInvoiceNumber, opt => opt.MapFrom(src => "PRO-INV-" + src.InvoiceNumber))
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => MapPayment(src.Payment)))
                .ForMember(dest => dest.Delivery, opt => opt.MapFrom(src => MapDelivery(src.Delivery)))
                .ForMember(dest => dest.TaxTotals, opt => opt.MapFrom(src => MapTaxTotals(src.TaxTotals)))
                .ForMember(dest => dest.TotalSalesAmount, opt => opt.MapFrom(src => src.TotalSalesAmount))
                .ForMember(dest => dest.TotalDiscountAmount, opt => opt.MapFrom(src => src.TotalDiscountAmount))
                .ForMember(dest => dest.NetAmount, opt => opt.MapFrom(src => src.NetAmount))
                .ForMember(dest => dest.TotalItemsDiscountAmount, opt => opt.MapFrom(src => src.TotalItemsDiscountAmount))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.Issuer, opt => opt.MapFrom(src => MapCompanyDto(src.Issuer)))
                .ForMember(dest => dest.Receiver, opt => opt.MapFrom(src => MapCompanyDto(src.Receiver)));
        }

        private PaymentDto MapPayment(Payment srcPayment)
        {
            if (srcPayment == null)
                return null;

            return new PaymentDto
            {
                BankName = srcPayment.BankName,
                BankAddress = srcPayment.BankAddress,
                BankAccountNo = srcPayment.BankAccountNo,
                BankAccountIBAN = srcPayment.BankAccountIBAN,
                SwiftCode = srcPayment.SwiftCode,
                Terms = srcPayment.Terms
            };
        }

        private DeliveryDto MapDelivery(Delivery srcDelivery)
        {
            if (srcDelivery == null)
                return null;

            return new DeliveryDto
            {
                Approach = srcDelivery.Approach,
                Packaging = srcDelivery.Packaging,
                DateValidity = srcDelivery.DateValidity.HasValue ? (DateTime?)srcDelivery.DateValidity.Value : null,
                ExportPort = srcDelivery.ExportPort,
                GrossWeight = srcDelivery.GrossWeight.HasValue ? (double?)srcDelivery.GrossWeight.Value : null,
                NetWeight = srcDelivery.NetWeight.HasValue ? (double?)srcDelivery.NetWeight.Value : null,
                Terms = srcDelivery.Terms
            };
        }

        private List<TaxTotalDto> MapTaxTotals(IReadOnlyCollection<TaxTotal> srcTaxTotals)
        {
            if (srcTaxTotals == null)
                return null;

            return srcTaxTotals.Select(taxTotal => new TaxTotalDto
            {
                TaxType = taxTotal.Code,
                Amount = taxTotal.Amount
            }).ToList();
        }

        private CompanyDto MapCompanyDto(Company company)
        {
            if (company is null)
                return null;

            return new CompanyDto
            {
                Address = MapAddressDto(company.Address),
                Type = company.Type.ToString(),
                Id = company.Id.ToString(),
                Name = company.Name
            };
        }

        private AddressDto MapAddressDto(Address address)
        {
            if (address == null)
                return null;

            return new AddressDto
            {
                BranchID = address.BranchId.ToString(),
                Country = address.Country,
                Governate = address.Governorate,
                RegionCity = address.RegionCity,
                Street = address.Street,
                BuildingNumber = address.BuildingNumber,
                PostalCode = address.PostalCode,
                Floor = address.Floor,
                Room = address.Room,
                Landmark = address.Landmark,
                AdditionalInformation = address.AdditionalInformation
            };
        }
    }
}
