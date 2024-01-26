using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace PasswordHasher
{
    public static class PasswordHasher
    {
        // Create a password hash
        public static (string passwordHash, string passwordSalt) CreatePasswordHash(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
           
            // Derive the password hash using PBKDF2 algorithm
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return (hashed, Convert.ToBase64String(salt));
        }

        // Verify the password hash
        public static bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
        {
            byte[] salt = Convert.FromBase64String(passwordSalt);

            // Derive a hash for the provided password using the same salt
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Compare the computed hash with the stored hash
            return hashed == passwordHash;
        }
    }
}
