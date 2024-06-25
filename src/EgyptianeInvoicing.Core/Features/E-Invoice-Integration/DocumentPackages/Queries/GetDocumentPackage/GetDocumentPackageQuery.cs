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
        public string Rid { get; set; }

        public GetDocumentPackageQuery(string rid)
        {
            Rid = rid;
        }
    }
}
