using System.Security.Cryptography;

namespace Cryptography.SymetricEncryption;

public static class AesEncryption
{
    public static byte[] Encrypt(byte[] data, byte[] key, byte[] initializationVector)
    {
        using var encryptor = Aes.Create();
        encryptor.Key = key;
        var encryptedData = encryptor.EncryptCbc(data, initializationVector);
        
        return encryptedData;
    }
    
    public static byte[] Decrypt(byte[] data,  byte[] key, byte[] initializationVector)
    {
        using var encryptor = Aes.Create();
        encryptor.Key = key;
        var decryptedData = encryptor.DecryptCbc(data, initializationVector);
        
        return decryptedData;
    }
}