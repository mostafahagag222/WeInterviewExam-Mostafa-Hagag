using System.Collections;
using System.Security.Cryptography;

namespace WeInterviewExam.Core;

public static class Helpers
{
    public static byte[] HashPassword(this string password, byte[] salt)
    {
        using var deriveBytes = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA512);
        return deriveBytes.GetBytes(64);
        
    }
    
    
    public static bool VerifyPassword(this string password, byte[] salt, byte[] storedHash)
    {
        byte[] computedHash = HashPassword(password, salt);
        return StructuralComparisons.StructuralEqualityComparer.Equals(computedHash, storedHash);
    }
    
}