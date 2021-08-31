using Application.Common.Helpers;

namespace Application.Common.Security
{
    public abstract class HashAlgorithm : IHashAlgorithm
    {
        public string CalculateHash(string plainText)
        {
            Require.NotNullOrEmpty(plainText, nameof(plainText));
            
            return CalculateHashInternal(plainText);
        }

        protected abstract string CalculateHashInternal(string plainText);
    }
}
