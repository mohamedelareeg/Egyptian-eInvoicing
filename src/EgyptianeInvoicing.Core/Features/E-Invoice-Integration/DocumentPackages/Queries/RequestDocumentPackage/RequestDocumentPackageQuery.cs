using BuildingBlocks.Messaging;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.RequestDocumentPackage.Request;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.RequestDocumentPackage.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentPackages.Queries.RequestDocumentPackage
{
    public class RequestDocumentPackageQuery : IQuery<PackageDownloadResponseDto>
    {
        public DocumentPackageRequestDto RequestDto { get; set; }

        public RequestDocumentPackageQuery(DocumentPackageRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }
}
