﻿using BuildingBlocks.Messaging;
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
        public string DocumentUUID { get; }

        public GetDocumentPdfQuery(string documentUUID)
        {
            DocumentUUID = documentUUID;
        }
    }
}