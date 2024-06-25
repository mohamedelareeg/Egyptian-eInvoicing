using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentRetrieval.GetDocument.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Documents.Queries.GetDocument
{
    public class GetDocumentQuery : IQuery<DocumentRetrievalDto>
    {
        public string DocumentUUID { get; }

        public GetDocumentQuery(string documentUUID)
        {
            DocumentUUID = documentUUID;
        }
    }
}
