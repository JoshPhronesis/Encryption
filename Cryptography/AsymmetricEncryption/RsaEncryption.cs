using System.Security.Cryptography;
using Cryptography.Extensions;

namespace Cryptography.AsymmetricEncryption;

public class RsaEncryption
{
    private readonly RSA _rsa; 

    public RsaEncryption()
    {
        _rsa = RSA.Create(2048);
    }
    public byte[] Encrypt(string dataToEncrypt)
    {
        return _rsa.Encrypt(dataToEncrypt.ToBytes(), RSAEncryptionPadding.OaepSHA256);
    }
    public byte[] Decrypt(byte[] dataToDecrypt)
    {
        return _rsa.Decrypt(dataToDecrypt, RSAEncryptionPadding.OaepSHA256);
    }
    public byte[] ExportPublicKey()
    {
        return _rsa.ExportRSAPublicKey();
    }
    public void ImportEncryptedPrivateKey(byte[] encryptedKey, string password)
    {
        _rsa.ImportEncryptedPkcs8PrivateKey(password.ToBytes(), encryptedKey, out _);
    }
    public void ImportPublicKey(byte[] publicKey)
    {
        _rsa.ImportRSAPublicKey(publicKey, out _);
    }
    public byte[] ExportPrivateKey(int numberOfIterations, string password)
    {
        var keyParams = new PbeParameters(
            PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, numberOfIterations);
        
        var encryptedPrivateKey = _rsa.ExportEncryptedPkcs8PrivateKey(
            password.ToBytes(), keyParams);

        return encryptedPrivateKey;
    }
}