using System;
using System.Security.Cryptography;
using System.Text;
using Application.Common.Security;

namespace Application.Common.Helpers
{
    public static class PasswordHelper
    {
        /// <exception cref="System.ArgumentException"><paramref name="length" /> must be > 0.</exception>
        /// <exception cref="System.Security.Cryptography.CryptographicException">
        ///     The cryptographic service provider (CSP) cannot be acquired.
        /// </exception>
        public static string GenerateSalt(int length)
        {
            Require.Satisfies(length, x => x > 0, "Value must be > 0.", nameof(length));

            using var cryptoService = new RNGCryptoServiceProvider();
            var saltBytes = new byte[length];
            cryptoService.GetNonZeroBytes(saltBytes);

            return Encoding.Unicode.GetString(saltBytes);
        }

        /// <exception cref="System.ArgumentNullException"></exception>
        public static string ComputeHash(string password, string salt)
        {
            Require.NotNull(password, nameof(password));
            Require.NotNull(salt, nameof(salt));

            var hashAlgorithm = new Sha512Hash();
            return hashAlgorithm.CalculateHash(password + salt);
        }

        /// <exception cref="System.ArgumentException"><paramref name="length" /> must be > 0.</exception>
        public static string Generate(int length)
        {
            Require.Satisfies(length, x => x > 0, "Value must be > 0.", nameof(length));

            var randomString = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Trim();
            return length <= randomString.Length ? randomString.Substring(0, length) : randomString;
        }
    }
}
