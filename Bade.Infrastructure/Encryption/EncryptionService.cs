using System.Security.Cryptography;

namespace Bade.Infrastructure.Encryption
{
    public class EncryptionService : IEncryptionService
    {
        public byte[] Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value)) return new byte[] {};
            var sha = new SHA1CryptoServiceProvider();
            return sha.ComputeHash(System.Text.Encoding.ASCII.GetBytes(value));
        }
    }
}