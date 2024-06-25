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
        public List<CreateEGSCodeUsageItemDto> Request { get; }

        public CreateEGSCodeUsageCommand(List<CreateEGSCodeUsageItemDto> request)
        {
            Request = request;
        }
    }
}
