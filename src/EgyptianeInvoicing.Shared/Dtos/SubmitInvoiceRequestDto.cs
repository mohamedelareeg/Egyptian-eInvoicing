using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Net.Pkcs11Interop.HighLevelAPI;

namespace EgyptianeInvoicing.Shared.Dtos
{
    public class SubmitInvoiceRequestDto
    {
        public Guid CompanyId { get; set; }
        public List<ImportedInvoiceDto> Invoices { get; set; }
        public IObjectHandle? certificate { get; set; } = null;
        public X509Certificate2? certForSigning { get; set; } = null;
    }
}
