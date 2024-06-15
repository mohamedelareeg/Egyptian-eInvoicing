using EgyptianeInvoicing.Core.Services.Abstractions;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EgyptianeInvoicing.Core.Services
{
    internal class SecureStorageService : ISecureStorageService
    {
        private const string TokenFilePath = "secure_token.dat";

        public void SaveToken(string accessToken)
        {
            byte[] encryptedToken = Protect(Encoding.UTF8.GetBytes(accessToken));
            File.WriteAllBytes(TokenFilePath, encryptedToken);
        }

        public string GetToken()
        {
            if (!File.Exists(TokenFilePath))
                return null;

            byte[] encryptedToken = File.ReadAllBytes(TokenFilePath);
            byte[] decryptedBytes = Unprotect(encryptedToken);
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        public void RemoveToken()
        {
            if (File.Exists(TokenFilePath))
                File.Delete(TokenFilePath);
        }

        private byte[] Protect(byte[] data)
        {
            byte[] entropy = new byte[20];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(entropy);
            }
            return ProtectedData.Protect(data, entropy, DataProtectionScope.CurrentUser);
        }

        private byte[] Unprotect(byte[] data)
        {
            byte[] entropy = new byte[20];
            return ProtectedData.Unprotect(data, entropy, DataProtectionScope.CurrentUser);
        }
    }
}
