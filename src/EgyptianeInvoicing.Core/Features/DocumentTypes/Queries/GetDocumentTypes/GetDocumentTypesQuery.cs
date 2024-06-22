using BuildingBlocks.Messaging;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.DocumentsType.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentTypes.Queries.GetDocumentTypes
{
    public class GetDocumentTypesQuery : IListQuery<DocumentTypeDto>
    {
    }
}
