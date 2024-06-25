using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetRecentDocuments.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentOperations.Queries.GetRecentDocuments
{
    public class GetRecentDocumentsQuery : IQuery<RecentDocumentsDto>
    {
        public DateTime? SubmissionDateFrom { get; set; }
        public DateTime? SubmissionDateTo { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNo { get; set; } = 1;
        public DateTime? IssueDateFrom { get; set; }
        public DateTime? IssueDateTo { get; set; }
        public string Direction { get; set; } = "";
        public string Status { get; set; } = "Valid";
        public string DocumentType { get; set; } = "";
        public string ReceiverType { get; set; } = "";
        public string ReceiverId { get; set; } = "";
        public string IssuerType { get; set; } = "";
        public string IssuerId { get; set; } = "";

    }
}
