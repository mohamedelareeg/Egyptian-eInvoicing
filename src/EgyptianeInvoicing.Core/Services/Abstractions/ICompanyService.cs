using BuildingBlocks.Results;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Services.Abstractions
{
    public interface ICompanyService
    {
        Task<Result<CompanyDto>> CreateCompanyAsync(
           string name,
           string phone,
           string email,
           string taxNumber,
           string commercialRegistrationNo,
           string branchID,
           string country,
           string governate,
           string regionCity,
           string street,
           string buildingNumber,
           string postalCode,
           string floor,
           string room,
           string landmark,
           string additionalInformation,
           string activityCode,
           string type,
           string clientId,
           string clientSecret1,
           string clientSecret2,
           string tokenPin,
           string certificate,
           List<PaymentDto> payments
       );
    }
}
