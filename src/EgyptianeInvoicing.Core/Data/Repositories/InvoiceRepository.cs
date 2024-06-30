using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using EgyptianeInvoicing.Core.Models;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InvoiceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Invoice invoice)
        {
            await _dbContext.Set<Invoice>().AddAsync(invoice);
        }

        public async Task RemoveAsync(Invoice invoice)
        {
            _dbContext.Set<Invoice>().Remove(invoice);
        }

        public async Task<Invoice> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<Invoice>()
                .Include(i => i.Payment)
                .Include(i => i.Delivery)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Invoice>> GetByPaginationAsync(
     DataTableOptionsDto options,
     InvoiceStatus? invoiceStatus,
     DocumentType? documentType,
     CompanyType? receiverType,
     Guid? receiverId,
     CompanyType? issuerType,
     Guid? issuerId,
     string? eInvoiceID,
     string? internalID)
        {
            var query = _dbContext.Set<Invoice>().Include(a=>a.TaxTotals).Include(i => i.Receiver).Include(i => i.Issuer)
                                  .AsQueryable();

            // Apply date range filter
            if (options.FromDate != null && options.ToDate != null)
            {
                query = query.Where(i => i.CreatedOnUtc >= options.FromDate && i.CreatedOnUtc <= options.ToDate);
            }
            else if (options.FromDate != null)
            {
                query = query.Where(i => i.CreatedOnUtc >= options.FromDate);
            }
            else if (options.ToDate != null)
            {
                query = query.Where(i => i.CreatedOnUtc <= options.ToDate);
            }

            if (invoiceStatus.HasValue)
            {
                query = query.Where(i => i.Status == invoiceStatus.Value);
            }

            if (documentType.HasValue)
            {
                query = query.Where(i => i.DocumentType == documentType.Value);
            }

            if (receiverType.HasValue)
            {
                query = query.Where(i => i.Receiver.Type == receiverType.Value);

                if (receiverId.HasValue && receiverId != Guid.Empty)
                {
                    query = query.Where(i => i.ReceiverId == receiverId.Value);
                }
            }

            if (issuerType.HasValue)
            {
                query = query.Where(i => i.Issuer.Type == issuerType.Value);
                if (issuerId.HasValue && issuerId != Guid.Empty)
                {
                    query = query.Where(i => i.IssuerId == issuerId.Value);
                }
            }

            if (!string.IsNullOrEmpty(eInvoiceID))
            {
                query = query.Where(i => i.EinvoiceId == eInvoiceID);
            }

            if (!string.IsNullOrEmpty(internalID))
            {
                query = query.Where(i => i.InvoiceNumber.Contains(internalID));
            }

            int totalCount = await query.CountAsync();

            if (!string.IsNullOrEmpty(options.OrderBy))
            {
                query = ApplyOrderBy(query, options.OrderBy);
            }

            query = query.Skip(options.Start)
                         .Take(options.Length);

            return await query.ToListAsync();
        }



        private IQueryable<Invoice> ApplyOrderBy(IQueryable<Invoice> query, string orderBy)
        {
            var parts = orderBy.Split(' ');
            var field = parts[0];
            var direction = parts.Length > 1 && parts[1].ToUpper() == "DESC" ? "Descending" : "Ascending";

            var parameter = Expression.Parameter(typeof(Invoice), "x");
            var property = Expression.Property(parameter, field);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = direction == "Descending" ? "OrderByDescending" : "OrderBy";
            var orderByExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { typeof(Invoice), property.Type },
                query.Expression,
                Expression.Quote(lambda)
            );

            return query.Provider.CreateQuery<Invoice>(orderByExpression);
        }


    }
}
