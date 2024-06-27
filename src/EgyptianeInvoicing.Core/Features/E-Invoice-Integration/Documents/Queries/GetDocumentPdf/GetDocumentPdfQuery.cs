using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Documents.Queries.GetDocumentPdf
{
    public class GetDocumentPdfQuery : IQuery<byte[]>
    {
        public GetDocumentPdfQuery(Guid companyId, string documentUUID)
        {
            CompanyId = companyId;
            DocumentUUID = documentUUID;
        }

        public Guid CompanyId { get; set; }
        public string DocumentUUID { get; }

    }
}
