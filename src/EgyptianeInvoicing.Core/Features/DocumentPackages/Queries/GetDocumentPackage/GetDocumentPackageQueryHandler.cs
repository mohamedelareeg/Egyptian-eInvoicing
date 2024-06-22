using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentPackages.Queries.GetDocumentPackage
{
    public class GetDocumentPackageCommandHandler : IQueryHandler<GetDocumentPackageQuery, byte[]>
    {
        private readonly IDocumentPackageClient _documentPackageClient;

        public GetDocumentPackageCommandHandler(IDocumentPackageClient documentPackageClient)
        {
            _documentPackageClient = documentPackageClient;
        }

        public async Task<Result<byte[]>> Handle(GetDocumentPackageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _documentPackageClient.GetDocumentPackageAsync(request.Rid);
                return Result.Success(response);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<byte[]>("GetDocumentPackageQuery", ex.Message);
            }
        }
    }
}
