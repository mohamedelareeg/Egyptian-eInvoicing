using BuildingBlocks.Extentions;
using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Common;
using EgyptianeInvoicing.Core.Clients.Common.Abstractions;
using EgyptianeInvoicing.Core.Services;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.DocumentsType.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentTypes.Queries.GetDocumentTypes
{
    public class GetDocumentTypesQueryHandler : IListQueryHandler<GetDocumentTypesQuery, DocumentTypeDto>
    {
        private readonly IDocumentTypesClient _documentTypesClient;

        public GetDocumentTypesQueryHandler(IDocumentTypesClient documentTypesClient)
        {
            _documentTypesClient = documentTypesClient;
        }

        public async Task<Result<CustomList<DocumentTypeDto>>> Handle(GetDocumentTypesQuery request, CancellationToken cancellationToken)
        {

            var result = await _documentTypesClient.GetDocumentTypesAsync();
            return result.ToCustomList();
        }
    }
}
