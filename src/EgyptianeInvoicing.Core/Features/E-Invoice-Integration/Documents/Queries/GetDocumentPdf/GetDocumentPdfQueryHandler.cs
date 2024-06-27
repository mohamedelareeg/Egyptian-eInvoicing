using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Documents.Queries.GetDocumentPdf
{
    public class GetDocumentPdfQueryHandler : IRequestHandler<GetDocumentPdfQuery, Result<byte[]>>
    {
        private readonly IDocumentRetrievalClient _client;

        public GetDocumentPdfQueryHandler(IDocumentRetrievalClient client)
        {
            _client = client;
        }

        public async Task<Result<byte[]>> Handle(GetDocumentPdfQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var pdfBytes = await _client.GetDocumentPdfAsync(request.CompanyId, request.DocumentUUID);
                return Result.Success(pdfBytes);
            }
            catch (Exception ex)
            {
                if (ex is HttpRequestException || ex is InvalidOperationException)
                {
                    return Result.Failure<byte[]>("GetDocumentPdfQuery", $"Failed to get PDF: {ex.Message}");
                }
                else if (ex is Exception && ex.Message == "Document not found.")
                {
                    return Result.Failure<byte[]>("GetDocumentPdfQuery", $"Document not found.");
                }
                else if (ex is Exception && ex.Message == "Document is not ready for download.")
                {
                    return Result.Failure<byte[]>("GetDocumentPdfQuery", $"Document is not ready for download.");
                }
                else if (ex is Exception && ex.Message == "Access to document is forbidden.")
                {
                    return Result.Failure<byte[]>("GetDocumentPdfQuery", $"Access to document is forbidden.");
                }
                else
                {
                    return Result.Failure<byte[]>("GetDocumentPdfQuery", $"Failed to get PDF: {ex.Message}");
                }
            }
        }
    }
}
