using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Data.Repositories.Abstractions;
using EgyptianeInvoicing.Core.Models;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using EgyptianeInvoicing.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Extentions;

namespace EgyptianeInvoicing.Core.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Company> GetByIdAsync(Guid id)
        {
            var company = await _context.Set<Company>()
                                         .Include(c => c.Address)
                                         .SingleOrDefaultAsync(c => c.Id == id);
            return company;
        }


        public async Task<List<Company>> GetAllAsync()
        {
            var companies = await _context.Set<Company>()
                                          .Include(c => c.Address)
                                          .ToListAsync();
            return companies;
        }


        public async Task AddAsync(Company company)
        {
            await _context.Set<Company>().AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Company company)
        {
            _context.Set<Company>().Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Company company)
        {
            _context.Set<Company>().Remove(company);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Set<Company>().AnyAsync(c => c.Id == id);
        }


        public async Task<Result<CustomList<Company>>> GetAllCompaniesAsync(DataTableOptionsDto options)
        {
            var query = _context.Set<Company>().Include(c => c.Address).AsQueryable();

            if (!string.IsNullOrEmpty(options.SearchText))
            {
                query = query.Where(c => c.Name.Contains(options.SearchText));
            }

            // Apply sorting if orderBy is provided (similar to the ApplyOrderBy method)
            // if (!string.IsNullOrEmpty(options.OrderBy))
            // {
            //     query = ApplyOrderBy(query, options.OrderBy);
            // }

            int totalCount = await query.CountAsync();

            int totalPages = totalCount > 0 ? (int)Math.Ceiling((double)totalCount / options.Length) : 0;

            var companies = await query
                .Skip(options.Start)
                .Take(options.Length)
                .ToListAsync();



            return Result.Success(companies.ToCustomList(totalCount, totalPages));
        }


        private IQueryable<Company> ApplyOrderBy(IQueryable<Company> query, string orderBy)
        {
            var parts = orderBy.Split(' ');
            var field = parts[0];
            var direction = parts.Length > 1 && parts[1].ToUpper() == "DESC" ? "Descending" : "Ascending";

            var parameter = Expression.Parameter(typeof(Company), "x");
            var property = Expression.Property(parameter, field);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = direction == "Descending" ? "OrderByDescending" : "OrderBy";
            var orderByExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { typeof(Company), property.Type },
                query.Expression,
                lambda
            );

            return query.Provider.CreateQuery<Company>(orderByExpression);
        }
        public async Task<string> GetCompanyTokenByIdAsync(Guid id)
        {
            var token = await _context.Set<Company>()
                                       .Where(c => c.Id == id)
                                       .Select(c => c.EInvoiceToken)
                                       .FirstOrDefaultAsync();
            return token;
        }
        public async Task<Result<bool>> SaveCompanyTokenAsync(Guid id, string eInvoiceToken)
        {
            var company = await _context.Set<Company>()
                                        .FirstOrDefaultAsync(c => c.Id == id);

            if (company == null)
                return Result.Failure<bool>("SaveCompanyTokenAsync", "Company not found.");

            company.SetEInvoiceToken(eInvoiceToken);

            try
            {
                await _context.SaveChangesAsync();
                return Result.Success(true);
            }
            catch (Exception ex)
            {
                return Result.Failure<bool>("SaveCompanyTokenAsync", ex.Message);
            }
        }
    }
}
