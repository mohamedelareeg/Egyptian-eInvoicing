using EgyptianeInvoicing.Core.Models;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Data.Abstractions.Repositories
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice);
        Task RemoveAsync(Invoice invoice);
        Task<Invoice> GetByIdAsync(Guid id);
        Task<List<Invoice>> GetByPaginationAsync(
              DataTableOptionsDto options,
              InvoiceStatus? invoiceStatus,
              DocumentType? documentType,
              CompanyType? receiverType,
              Guid? receiverId,
              CompanyType? issuerType,
              Guid? issuerId,
              string? eInvoiceID,
              string? internalID);
    }
}
