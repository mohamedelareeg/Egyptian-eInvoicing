using BuildingBlocks.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EgyptianeInvoicing.Core.Features.Authontication.Commands.Authenticate
{
    public class AuthenticateCommand : ICommand<string>
    {
        public AuthenticateCommand(Guid companyId)
        {
            CompanyId = companyId;
        }

        public Guid CompanyId { get; }
      
    }
}
