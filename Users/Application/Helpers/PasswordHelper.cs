using System.Security.Cryptography;

namespace BillEase360_CodeFirstApproach.Users.Application.Helpers
{
    public class PasswordHelper
    {

        public static string HashPassword(string password)
        {
            // Generate a 16-byte salt
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[16];
                rng.GetBytes(salt);

                // Derive a 32-byte key using PBKDF2
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
                {
                    byte[] hash = pbkdf2.GetBytes(32);

                    // Store salt + hash together (Base64 encode)
                    byte[] hashBytes = new byte[48]; // 16 (salt) + 32 (hash)
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 32);

                    return Convert.ToBase64String(hashBytes);
                }
            }
        }


        // Verify plain password against stored salted hash
        public static bool VerifyPassword(string password, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            // Extract salt (first 16 bytes)
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Extract stored hash (last 32 bytes)
            byte[] storedSubKey = new byte[32];
            Array.Copy(hashBytes, 16, storedSubKey, 0, 32);

            // Hash input password with extracted salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] computedSubKey = pbkdf2.GetBytes(32);

                // Compare stored hash vs computed hash
                return CryptographicOperations.FixedTimeEquals(storedSubKey, computedSubKey);
            }
        }
    }

   
}
