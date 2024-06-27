using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetSubmission.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentOperations.Queries.GetSubmission
{
    public class GetSubmissionQuery : IQuery<GetSubmissionResponseDto>
    {
        public GetSubmissionQuery(Guid companyId, string submissionUUID, string pageSize, string pageNo)
        {
            CompanyId = companyId;
            SubmissionUUID = submissionUUID;
            PageSize = pageSize;
            PageNo = pageNo;
        }

        public Guid CompanyId { get; set; }
        public string SubmissionUUID { get; set; }
        public string PageSize { get; set; } = "10";
        public string PageNo { get; set; } = "1";
    }
}
