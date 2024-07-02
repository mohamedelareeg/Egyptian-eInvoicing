using BuildingBlocks.Results;
using EgyptianeInvoicing.Shared.Dtos.SignerDto;
using Net.Pkcs11Interop.HighLevelAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static EgyptianeInvoicing.Signer.Services.TokenSigningService;

namespace EgyptianeInvoicing.Signer.Services.Abstractions
{
    public interface ISigningService
    {
        Result<string> SignWithCMS(string serializedJson, TokenSigningSettingsDto tokenSigningSettings, string tokenPin, string certificate);
        Result<SigningResultDto> Sign(string dllPath, string tokenPin, string certificate);
        Result<string> UseSignature(string serializedJson, IObjectHandle certificate, X509Certificate2 certForSigning);
    }
}
