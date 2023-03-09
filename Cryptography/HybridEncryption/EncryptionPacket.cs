namespace Cryptography.HybridEncryption;

public class EncryptionPacket
{
    public byte[] EncryptedSessionKey;
    public byte[] EncryptedData;
    public byte[] InitializationVector;
}