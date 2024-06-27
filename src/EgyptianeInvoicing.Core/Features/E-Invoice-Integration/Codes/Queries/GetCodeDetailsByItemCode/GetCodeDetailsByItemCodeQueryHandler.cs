using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.GetCodeDetailsByItemCode.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Queries.GetCodeDetailsByItemCode
{
    public class GetCodeDetailsByItemCodeQueryHandler : IQueryHandler<GetCodeDetailsByItemCodeQuery, GetCodeDetailsResponseDto>
    {
        private readonly ICodeManagementClient _codeManagementClient;

        public GetCodeDetailsByItemCodeQueryHandler(ICodeManagementClient codeManagementClient)
        {
            _codeManagementClient = codeManagementClient;
        }

        public async Task<Result<GetCodeDetailsResponseDto>> Handle(GetCodeDetailsByItemCodeQuery request, CancellationToken cancellationToken)
        {
            var response = await _codeManagementClient.GetCodeDetailsByItemCodeAsync(request.CompanyId, request.CodeType, request.ItemCode);
            return response;
        }
    }
}
