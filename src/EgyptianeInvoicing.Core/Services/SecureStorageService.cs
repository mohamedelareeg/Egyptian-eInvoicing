using EgyptianeInvoicing.Core.Services.Abstractions;
using System;
using System.IO;
using System.Text;

namespace EgyptianeInvoicing.Core.Services
{
    internal class SecureStorageService : ISecureStorageService
    {
        private const string TokenFilePath = "secure_token.txt";

        public void SaveToken(string accessToken)
        {
            File.WriteAllText(TokenFilePath, accessToken);
        }

        public string GetToken()
        {
            try
            {
                if (!File.Exists(TokenFilePath))
                    return null;

                return File.ReadAllText(TokenFilePath);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve token.", ex);
            }
        }

        public void RemoveToken()
        {
            if (File.Exists(TokenFilePath))
                File.Delete(TokenFilePath);
        }
    }
}
