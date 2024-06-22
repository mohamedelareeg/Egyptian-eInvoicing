using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.UpdateEGSCodeUsage.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EgyptianeInvoicing.Core.Features.Codes.Commands.UpdateEGSCodeUsage
{
    public class UpdateEGSCodeUsageCommand : ICommand<bool>
    {
        public int CodeUsageRequestId { get; set; }
        public UpdateEGSCodeUsageRequestDto Request { get; set; }
    }
}
