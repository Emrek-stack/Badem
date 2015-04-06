namespace Bade.Infrastructure.Encryption
{

    public class EncryptedString
    {
        public EncryptedString(byte[] encryptedValue)
        {
            EncryptedValue = encryptedValue;
        }

        public byte[] EncryptedValue { get; private set; }

        public static EncryptedString Create(string value, IEncryptionService encryptionService)
        {
            return new EncryptedString(encryptionService.Encrypt(value));
        }
    }
}