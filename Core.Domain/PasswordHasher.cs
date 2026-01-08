using System.Security.Cryptography;
using System.Text;

namespace Area52.Core.Domain;

public static class PasswordHasher
{
    public static string Hash(string password)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToHexString(hash);
    }

    public static bool Verify(string password, string storedHash)
    {
        var hashed = Hash(password);
        return string.Equals(hashed, storedHash, StringComparison.OrdinalIgnoreCase);
    }
}