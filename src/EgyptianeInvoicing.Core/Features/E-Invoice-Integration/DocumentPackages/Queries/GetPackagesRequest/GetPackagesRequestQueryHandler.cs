using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.GetPackagesRequest.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentPackages.Queries.GetPackagesRequest
{
    public class GetPackagesRequestQueryHandler : IQueryHandler<GetPackagesRequestQuery, DocumentPackageResponseDto>
    {
        private readonly IDocumentPackageClient _documentPackageClient;

        public GetPackagesRequestQueryHandler(IDocumentPackageClient documentPackageClient)
        {
            _documentPackageClient = documentPackageClient;
        }

        public async Task<Result<DocumentPackageResponseDto>> Handle(GetPackagesRequestQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _documentPackageClient.GetPackagesRequestAsync(
                    request.PageSize,
                    request.PageNo,
                    request.DateFrom,
                    request.DateTo,
                    request.DocumentTypeName,
                    request.Statuses,
                    request.ProductsInternalCodes,
                    request.ReceiverSenderType,
                    request.ReceiverSenderId,
                    request.BranchNumber,
                    request.ItemCodes
                );
                return response;
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<DocumentPackageResponseDto>("GetPackagesRequestQuery", ex.Message);
            }
        }
    }
}
