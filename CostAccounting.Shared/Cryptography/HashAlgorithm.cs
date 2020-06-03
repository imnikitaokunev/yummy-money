namespace CostAccounting.Shared.Cryptography
{
    public abstract class HashAlgorithm : IHashAlgorithm
    {
        public string CalculateHash(string plainText)
        {
            Expect.ArgumentNotNull(plainText, nameof(plainText));
            Expect.ArgumentSatisfies(plainText, x => x.Length > 0, "Length must be > 0", nameof(plainText));

            return CalculateHashInternal(plainText);
        }

        protected abstract string CalculateHashInternal(string plainText);
    }
}