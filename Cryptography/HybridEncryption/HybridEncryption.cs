using System.Security.Cryptography;
using Cryptography.SymmetricEncryption;

namespace Cryptography.HybridEncryption;

public class HybridEncryption
{
    public static EncryptionPacket EncryptData(byte[] original, AsymmetricEncryption.RsaEncryption rsaEncryptor)
    {
        // Generate our session key.
        var encryptionKey = RandomNumberGenerator.GetBytes(32);

        // Create the encrypted packet and generate the IV.
        var encryptedPacket = new EncryptionPacket { InitializationVector = RandomNumberGenerator.GetBytes(16) };

        // Encrypt our data with AES.
        encryptedPacket.EncryptedData = AesEncryption.Encrypt(original, encryptionKey, encryptedPacket.InitializationVector);

        // Encrypt the session key with RSA
        encryptedPacket.EncryptedSessionKey = rsaEncryptor.Encrypt(encryptionKey);

        return encryptedPacket;
    }
    
    public static byte[] DecryptData(EncryptionPacket encryptedPacket, AsymmetricEncryption.RsaEncryption rsaEncryptor)
    {
        // Decrypt AES Key with RSA.
        var decryptedSessionKey = rsaEncryptor.Decrypt(encryptedPacket.EncryptedSessionKey);

        // Decrypt our data with  AES using the decrypted session key.
        var decryptedData = AesEncryption.Decrypt(encryptedPacket.EncryptedData,
            decryptedSessionKey, encryptedPacket.InitializationVector);

        return decryptedData;
    }
}