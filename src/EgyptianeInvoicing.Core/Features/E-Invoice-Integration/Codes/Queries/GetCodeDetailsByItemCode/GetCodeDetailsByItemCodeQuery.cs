using BuildingBlocks.Messaging;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.GetCodeDetailsByItemCode.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Queries.GetCodeDetailsByItemCode
{
    public class GetCodeDetailsByItemCodeQuery : IQuery<GetCodeDetailsResponseDto>
    {
        public GetCodeDetailsByItemCodeQuery(Guid companyId, string codeType, string itemCode)
        {
            CompanyId = companyId;
            CodeType = codeType;
            ItemCode = itemCode;
        }

        public Guid CompanyId { get; set; }
        public string CodeType { get; set; }
        public string ItemCode { get; set; }
    }
}
