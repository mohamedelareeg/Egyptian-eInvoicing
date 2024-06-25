using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.ValueObjects;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using EgyptianeInvoicing.Shared.Enums;
using EgyptianeInvoicing.Shared.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EgyptianeInvoicing.Core.Features.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommand : ICommand<bool>
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Phone { get; }
        public string Email { get; }
        public string TaxNumber { get; }
        public string CommercialRegistrationNo { get; }
        public AddressDto Address { get; }
        public CompanyType Type { get; }
        public ClientCredentialsDto Credentials { get; }
        public List<PaymentDto> Payments { get; }
        public UpdateCompanyCommand(UpdateCompanyRequestDto requestDto)
        {
            Id = requestDto.Id;
            Name = requestDto.Name;
            Phone = requestDto.Phone;
            Email = requestDto.Email;
            TaxNumber = requestDto.TaxNumber;
            CommercialRegistrationNo = requestDto.CommercialRegistrationNo;
            Address = requestDto.Address;
            Type = requestDto.Type;
            Credentials = requestDto.Credentials;
            Payments = requestDto.Payments;
        }
    }
}
