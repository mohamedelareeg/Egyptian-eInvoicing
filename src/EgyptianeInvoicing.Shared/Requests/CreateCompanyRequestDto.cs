using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using EgyptianeInvoicing.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Shared.Requests
{
    public class CreateCompanyRequestDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string TaxNumber { get; set; }
        public string CommercialRegistrationNo { get; set; }
        public AddressDto Address { get; set; }
        public CompanyType Type { get; set; }
        public ClientCredentialsDto Credentials { get; set; }
        public List<PaymentDto> Payments { get; set; }
        public string ActivityCode { get; set; }
    }
}
