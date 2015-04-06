namespace Bade.Infrastructure.Encryption
{
    public interface IEncryptionService
    {
        byte[] Encrypt(string value);
    }
}