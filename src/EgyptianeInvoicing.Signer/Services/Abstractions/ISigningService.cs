using BuildingBlocks.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Signer.Services.Abstractions
{
    public interface ISigningService
    {
        Result<string> SignWithCMS(string serializedJson, string dllLibPath, string tokenPin, string tokenCertificate);
    }
}
