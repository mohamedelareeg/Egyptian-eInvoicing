using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Shared.Dtos
{
    public class ClientCredentialsDto
    {
        public string ClientId { get; set; }
        public string ClientSecret1 { get; set; }
        public string ClientSecret2 { get; set; }
        public string TokenPin { get; set; }
        public string Certificate { get; set; }
    }
}
