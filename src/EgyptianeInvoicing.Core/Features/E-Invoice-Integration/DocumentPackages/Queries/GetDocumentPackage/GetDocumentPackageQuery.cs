using BuildingBlocks.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentPackages.Queries.GetDocumentPackage
{
    public class GetDocumentPackageQuery : IQuery<byte[]>
    {
        public GetDocumentPackageQuery(Guid companyId, string rid)
        {
            CompanyId = companyId;
            Rid = rid;
        }

        public Guid CompanyId { get; set; }
        public string Rid { get; set; }

    }
}
