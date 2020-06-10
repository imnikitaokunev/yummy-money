namespace CostAccounting.Shared.Cryptography
{
    public interface IHashAlgorithm
    {
        /// <summary>
        ///     Calculates hash of the text.
        /// </summary>
        /// <param name="plainText">Text whose hash is to be calculated.</param>
        /// <returns>Hash of the passed text.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="plainText" /> is null.</exception>
        /// <exception cref="System.ArgumentException">Length of <paramref name="plainText" /> nameof(plainText).</exception>
        string CalculateHash(string plainText);
    }
}