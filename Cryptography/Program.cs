using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Cryptography.AsymmetricEncryption;
using Cryptography.Extensions;
using Cryptography.HybridEncryption;
using Cryptography.SymmetricEncryption;

// testing our hybrid encryption
TestHybridEncryption();

// testing our symmetric encryption
TestSymmetricEncryption();

// testing our asymmetric encryption
TestAsymmetricEncryption();

Debugger.Break();

void TestHybridEncryption()
{
    Console.WriteLine("********* Initializing Hybrid encryption test");
    const string message = "Hello World!";
    Console.WriteLine($"Original data: {message}");

    // initialize our rsa encryptor
    var rsaEncryptor = new RsaEncryption();

    // encrypt our data
    var encryptionPacket = HybridEncryption.EncryptData(message.ToBytes(), rsaEncryptor);
    Console.WriteLine($"Encrypted data: {Convert.ToBase64String(encryptionPacket.EncryptedData)}");

    // decrypt our data
    var decrypted = HybridEncryption.DecryptData(encryptionPacket, rsaEncryptor).BytesToString();
    Console.WriteLine($"Decrypted data: {decrypted}");
}

void TestAsymmetricEncryption()
{
    Console.WriteLine("********* Initializing Asymmetric encryption test");
    const string original = "Hello World!";
    Console.WriteLine($"Original data: {original}");
    
    // initialize our encryptor
    var rsa = new RsaEncryption();

    // create our private key
    byte[] encryptedPrivateKey = rsa.ExportPrivateKey(100000, "bjbh2j3hbjbjk33iui5br");

    // export our public key
    byte[] publicKey = rsa.ExportPublicKey();
    // our data to encrypt

    // encrypt our data
    var encrypted = rsa.Encrypt(original);
    Console.WriteLine($"Encrypted data: {Convert.ToBase64String(encrypted)}");

    // create new encryptor to decrypt our data
    var rsa2 = new RsaEncryption();

    // import our public key
    rsa2.ImportPublicKey(publicKey);

    // import the mathematically linked private key
    rsa2.ImportEncryptedPrivateKey(encryptedPrivateKey, "bjbh2j3hbjbjk33iui5br");

    // decrypt our data
    var decrypted = rsa2.Decrypt(encrypted).BytesToString();
    Console.WriteLine($"Decrypted data: {decrypted}");
}

void TestSymmetricEncryption()
{
    Console.WriteLine("********* Initializing Symmetric encryption test");
    var message = "Hello world!";
    Console.WriteLine($"Original data: {message}");

// generate our initialization vector
    var initializationVector = RandomNumberGenerator.GetBytes(16);

// generate our key
    var key = RandomNumberGenerator.GetBytes(32);

// encrypt our data
    var encryptedData = AesEncryption.Encrypt(message.ToBytes(), key, initializationVector);
    Console.WriteLine($"Encrypted data: {Convert.ToBase64String(encryptedData)}");

// decrypt our data
    var decryptedData = AesEncryption.Decrypt(encryptedData, key, initializationVector);
    Console.WriteLine($"Decrypted data: {decryptedData.BytesToString()}");
}