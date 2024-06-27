using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Models;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Data.Repositories.Abstractions
{
    public interface ICompanyRepository
    {
        Task<Company> GetByIdAsync(Guid id);
        Task<List<Company>> GetAllAsync();
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(Company company);
        Task<bool> ExistsAsync(Guid id);
        Task<Result<CustomList<Company>>> GetAllCompaniesAsync(DataTableOptionsDto options);
        Task<string> GetCompanyTokenByIdAsync(Guid id);
        Task<Result<bool>> SaveCompanyTokenAsync(Guid id, string eInvoiceToken);

    }
}
