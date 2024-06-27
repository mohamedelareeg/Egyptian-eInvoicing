using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocumentDetails.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Documents.Queries.GetDocumentDetails
{
    public class GetDocumentDetailsQuery : IQuery<DocumentDetailsDto>
    {
        public GetDocumentDetailsQuery(Guid companyId, string documentUUID)
        {
            CompanyId = companyId;
            DocumentUUID = documentUUID;
        }

        public Guid CompanyId { get; set; }
        public string DocumentUUID { get; }

    }
}
