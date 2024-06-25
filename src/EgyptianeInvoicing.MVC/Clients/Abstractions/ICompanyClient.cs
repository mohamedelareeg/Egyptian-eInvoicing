using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Requests;
using EgyptianeInvoicing.MVC.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.MVC.Clients.Abstractions
{
    public interface ICompanyClient
    {
        Task<BaseResponse<CompanyDto>> CreateCompanyAsync(CreateCompanyRequestDto request);
        Task<BaseResponse<CompanyDto>> GetCompanyAsync(Guid id);
        Task<BaseResponse<CustomList<CompanyDto>>> SearchCompaniesAsync(DataTableOptionsDto options);
        Task<BaseResponse<bool>> UpdateCompanyAsync(UpdateCompanyRequestDto request);
        Task<BaseResponse<bool>> DeleteCompanyAsync(Guid id);
    }
}
