using BuildingBlocks.Extentions;
using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.SearchCodeUsage.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Queries.SearchCodeUsageRequests
{
    public class SearchCodeUsageRequestsHandler : IListQueryHandler<SearchCodeUsageRequestsQuery, CodeUsageRequestDetailsDto>
    {
        private readonly ICodeManagementClient _codeManagementClient;

        public SearchCodeUsageRequestsHandler(ICodeManagementClient codeManagementClient)
        {
            _codeManagementClient = codeManagementClient;
        }

        public async Task<Result<CustomList<CodeUsageRequestDetailsDto>>> Handle(SearchCodeUsageRequestsQuery request, CancellationToken cancellationToken)
        {
            var result = await _codeManagementClient.SearchCodeUsageRequestsAsync(request.Active,request.Status,request.PageSize,request.PageNumber,request.OrderDirection);
            return result.ToCustomList();
        }
    }
}
