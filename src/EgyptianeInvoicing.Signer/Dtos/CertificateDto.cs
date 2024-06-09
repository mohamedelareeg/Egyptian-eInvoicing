using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Signer.Dtos
{
    public class CertificateDto
    {
        public string ContentType { get; set; }
        public string FriendlyName { get; set; }
        public bool Verified { get; set; }
        public string SimpleName { get; set; }
        public string SignatureAlgorithm { get; set; }
        public string PublicKey { get; set; }
        public bool Archived { get; set; }
        public int RawDataLength { get; set; }
    }
}
