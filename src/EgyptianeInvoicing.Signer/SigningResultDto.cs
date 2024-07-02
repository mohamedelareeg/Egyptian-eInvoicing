using Net.Pkcs11Interop.HighLevelAPI;
using System.Security.Cryptography.X509Certificates;

namespace EgyptianeInvoicing.Signer
{
    public class SigningResultDto
    {
        public IObjectHandle CertificateHandle { get; set; }
        public X509Certificate2 CertificateForSigning { get; set; }
    }
}
