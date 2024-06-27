using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentOperations.Queries.SearchDocuments
{
    public class SearchDocumentsQuery : IQuery<RecentDocumentsDto>
    {
       

        public Guid CompanyId { get; set; }
        public DateTime? SubmissionDateFrom { get; set; }
        public DateTime? SubmissionDateTo { get; set; }
        public DateTime? IssueDateFrom { get; set; }
        public DateTime? IssueDateTo { get; set; }
        public string ContinuationToken { get; set; } = "";
        public int PageSize { get; set; } = 10;
        public string Direction { get; set; } = "";
        public string Status { get; set; } = "Valid";
        public string DocumentType { get; set; } = "";
        public string ReceiverType { get; set; } = "";
        public string ReceiverId { get; set; } = "";
        public string IssuerType { get; set; } = "";
        public string IssuerId { get; set; } = "";
        public string UUID { get; set; } = "";
        public string InternalID { get; set; } = "";
    }
}
