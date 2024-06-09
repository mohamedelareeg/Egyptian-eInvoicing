using BuildingBlocks.Results;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Signer.Services.Abstractions
{
    public interface ISerializationService
    {
        Result<string> Serialize(JObject request);
    }
}
