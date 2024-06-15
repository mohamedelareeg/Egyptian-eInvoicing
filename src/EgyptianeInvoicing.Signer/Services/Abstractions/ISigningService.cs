using BuildingBlocks.Results;
using EgyptianeInvoicing.Shared.Dtos.SignerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Signer.Services.Abstractions
{
    public interface ISigningService
    {
        Result<string> SignWithCMS(string serializedJson, TokenSigningSettingsDto tokenSigningSettings);
    }
}
