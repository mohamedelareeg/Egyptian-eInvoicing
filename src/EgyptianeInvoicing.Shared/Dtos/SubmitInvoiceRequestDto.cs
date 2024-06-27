using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Shared.Dtos
{
    public class SubmitInvoiceRequestDto
    {
        public Guid CompanyId { get; set; }
        public List<ImportedInvoiceDto> Invoices { get; set; }
    }
}
