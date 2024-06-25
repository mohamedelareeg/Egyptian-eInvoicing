using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using EgyptianeInvoicing.Shared.Enums;
using System;
using System.Collections.Generic;

namespace EgyptianeInvoicing.Shared.Requests
{
    public class UpdateCompanyRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string TaxNumber { get; set; }
        public string CommercialRegistrationNo { get; set; }
        public AddressDto Address { get; set; }
        public CompanyType Type { get; set; }
        public ClientCredentialsDto Credentials { get; set; }
        public List<PaymentDto> Payments { get; set; }
    }
}
