using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.CreateEGSCode.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EgyptianeInvoicing.Core.Features.Codes.Commands.CreateEGSCodeUsage
{
    public class CreateEGSCodeUsageCommand : ICommand<bool>
    {
        public CreateEGSCodeUsageCommand(Guid companyId, List<CreateEGSCodeUsageItemDto> request)
        {
            CompanyId = companyId;
            Request = request;
        }

        public Guid CompanyId { get; set; }
        public List<CreateEGSCodeUsageItemDto> Request { get; }

    
    }
}
