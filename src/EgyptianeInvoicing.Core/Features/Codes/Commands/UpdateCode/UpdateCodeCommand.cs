using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.UpdateCode.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EgyptianeInvoicing.Core.Features.Codes.Commands.UpdateCode
{
    public class UpdateCodeCommand : ICommand<bool>
    {
        public string CodeType { get; set; }
        public string ItemCode { get; set; }
        public CodeUpdateRequestDto Request { get; set; }
    }
}
