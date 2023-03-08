using System.Diagnostics;
using System.Security.Cryptography;
using Cryptography.AsymmetricEncryption;
using Cryptography.Extensions;
using Cryptography.SymetricEncryption;

// testing our symmetric encryption
TestSymmetricEncryption();

// testing our asymmetric encryption
TestAsymmetricEncryption();

Debugger.Break();

void TestAsymmetricEncryption()
{
    // initialize our encryptor
    var rsa = new RsaEncryption();
    
    // create our private key
    byte[] encryptedPrivateKey = rsa.ExportPrivateKey(100000, "bjbh2j3hbjbjk33iui5br");
    
    // export our public key
    byte[] publicKey = rsa.ExportPublicKey();
    // our data to encrypt
    const string original = "Hello World!";
    
    // encrypt our data
    var encrypted = rsa.Encrypt(original);

    // create new encryptor to decrypt our data
    var rsa2 = new RsaEncryption();
    
    // import our public key
    rsa2.ImportPublicKey(publicKey);
    
    // import the mathematically linked private key
    rsa2.ImportEncryptedPrivateKey(encryptedPrivateKey, "bjbh2j3hbjbjk33iui5br");

    // decrypt our data
    var decrypted = rsa2.Decrypt(encrypted).BytesToString();
}

void TestSymmetricEncryption()
{
    var message = "Hello world!";

// generate our initialization vector
    var initializationVector = RandomNumberGenerator.GetBytes(16);

// generate our key
    var key = RandomNumberGenerator.GetBytes(32);

// encrypt our data
    var encryptedData = AesEncryption.Encrypt(message.ToBytes(), key, initializationVector);
    Console.WriteLine(Convert.ToBase64String(encryptedData));

// decrypt our data
    var decryptedData = AesEncryption.Decrypt(encryptedData, key, initializationVector);
    Console.WriteLine(decryptedData.BytesToString());
}