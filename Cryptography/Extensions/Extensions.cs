using System.Text;

namespace Cryptography.Extensions;

public static class Extensions
{
    public static byte[] ToBytes(this string input)
    {
        return Encoding.UTF8.GetBytes(input);
    }

    public static string ToMessage(this byte[] data)
    {
        return Encoding.UTF8.GetString(data);
    }
}