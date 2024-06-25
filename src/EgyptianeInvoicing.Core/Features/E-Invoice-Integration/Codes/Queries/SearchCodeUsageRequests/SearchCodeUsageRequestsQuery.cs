using BuildingBlocks.Messaging;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.SearchCodeUsage.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Queries.SearchCodeUsageRequests
{
    public class SearchCodeUsageRequestsQuery : IListQuery<CodeUsageRequestDetailsDto>
    {
        public string Active { get; set; }
        public string Status { get; set; }
        public string PageSize { get; set; }
        public string PageNumber { get; set; }
        public string OrderDirection { get; set; }
    }
}
