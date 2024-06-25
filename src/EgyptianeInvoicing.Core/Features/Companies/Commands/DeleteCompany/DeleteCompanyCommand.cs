using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EgyptianeInvoicing.Core.Features.Companies.Commands.DeleteCompany
{
    public class DeleteCompanyCommand : ICommand<bool>
    {
        public Guid Id { get; set; }
    }
}
