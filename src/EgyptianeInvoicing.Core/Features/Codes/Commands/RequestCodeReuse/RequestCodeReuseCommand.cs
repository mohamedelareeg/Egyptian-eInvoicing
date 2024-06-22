﻿using BuildingBlocks.Messaging;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.RequestCodeReuse.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EgyptianeInvoicing.Core.Features.Codes.Commands.RequestCodeReuse
{
    public class RequestCodeReuseCommand : ICommand<bool>
    {
        public List<CodeUsageItemDto> Request { get; set; }
    }
}
