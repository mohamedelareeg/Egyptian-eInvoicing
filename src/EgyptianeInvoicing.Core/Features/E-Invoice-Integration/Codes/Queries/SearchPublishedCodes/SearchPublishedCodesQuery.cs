using BuildingBlocks.Messaging;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.SearchPublishedCodes.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Codes.Queries.SearchPublishedCodes
{
    public class SearchPublishedCodesQuery : IListQuery<PublishedCodeDto>
    {
        public SearchPublishedCodesQuery(Guid companyId, string codeType, string parentLevelName, bool onlyActive, DateTime activeFrom, int pageSize, int pageNumber)
        {
            CompanyId = companyId;
            CodeType = codeType;
            ParentLevelName = parentLevelName;
            OnlyActive = onlyActive;
            ActiveFrom = activeFrom;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public Guid CompanyId { get; set; }
        public string CodeType { get; set; }
        public string ParentLevelName { get; set; }
        public bool OnlyActive { get; set; }
        public DateTime ActiveFrom { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
