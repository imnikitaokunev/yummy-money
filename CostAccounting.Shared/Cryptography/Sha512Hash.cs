using System.Security.Cryptography;
using System.Text;

namespace CostAccounting.Shared.Cryptography
{
    public sealed class Sha512Hash : HashAlgorithm
    {
        protected override string CalculateHashInternal(string plainText)
        {
            var bytes = Encoding.Unicode.GetBytes(plainText);
            using var sha512 = SHA512.Create();
            var hashed = sha512.ComputeHash(bytes);
            return Encoding.Unicode.GetString(hashed);
        }
    }
}