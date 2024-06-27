using BuildingBlocks.Extentions;
using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.SearchPublishedCodes.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Queries.SearchPublishedCodes
{
    public class SearchPublishedCodesQueryHandler : IListQueryHandler<SearchPublishedCodesQuery, PublishedCodeDto>
    {
        private readonly ICodeManagementClient _codeManagementClient;

        public SearchPublishedCodesQueryHandler(ICodeManagementClient codeManagementClient)
        {
            _codeManagementClient = codeManagementClient;
        }

        public async Task<Result<CustomList<PublishedCodeDto>>> Handle(SearchPublishedCodesQuery request, CancellationToken cancellationToken)
        {
            var response = await _codeManagementClient.SearchPublishedCodesAsync(
                request.CompanyId,
                request.CodeType,
                request.ParentLevelName,
                request.OnlyActive,
                request.ActiveFrom,
                request.PageSize,
                request.PageNumber);

            return response.ToCustomList();
        }
    }
}
