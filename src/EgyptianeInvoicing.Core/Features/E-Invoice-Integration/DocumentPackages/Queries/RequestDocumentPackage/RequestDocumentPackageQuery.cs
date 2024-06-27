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
        public RequestDocumentPackageQuery(Guid companyId, DocumentPackageRequestDto requestDto)
        {
            CompanyId = companyId;
            RequestDto = requestDto;
        }

        public Guid CompanyId { get; set; }
        public DocumentPackageRequestDto RequestDto { get; set; }
    }
}
