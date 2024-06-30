using BuildingBlocks.Messaging;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Enums;
using System;

namespace EgyptianeInvoicing.Core.Features.Invoices.Queries.SearchInvoices
{
    public class SearchInvoicesQuery : IListQuery<InvoiceDto>
    {
        public DataTableOptionsDto Options { get; set; }
        public InvoiceStatus? InvoiceStatus { get; set; }
        public DocumentType? DocumentType { get; set; }
        public CompanyType? ReceiverType { get; set; }
        public Guid? ReceiverId { get; set; } = Guid.Empty;
        public CompanyType? IssuerType { get; set; }
        public Guid? IssuerId { get; set; } = Guid.Empty;
        public string? EInvoiceId { get; set; } = "";
        public string? InternalID { get; set; } = "";
    }
}
