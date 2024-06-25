using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.RequestDocumentPackage.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentPackages.Queries.RequestDocumentPackage
{
    public class RequestDocumentPackageQueryHandler : IQueryHandler<RequestDocumentPackageQuery, PackageDownloadResponseDto>
    {
        private readonly IDocumentPackageClient _documentPackageClient;

        public RequestDocumentPackageQueryHandler(IDocumentPackageClient documentPackageClient)
        {
            _documentPackageClient = documentPackageClient;
        }

        public async Task<Result<PackageDownloadResponseDto>> Handle(RequestDocumentPackageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _documentPackageClient.RequestDocumentPackageAsync(request.RequestDto);
                return Result.Success(response);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<PackageDownloadResponseDto>("RequestDocumentPackageCommand", ex.Message);
            }
        }
    }
}
