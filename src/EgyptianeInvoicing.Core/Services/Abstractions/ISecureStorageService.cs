using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Services.Abstractions
{
    public interface ISecureStorageService
    {
        void SaveToken(string accessToken);
        string GetToken();
        void RemoveToken();
    }
}
