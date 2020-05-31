using System;

namespace CostAccounting.Shared
{
    public static class Expect
    {
        /// <summary>
        /// Throws <see cref="System.ArgumentNullException" /> if the given argument is null.
        /// </summary>
        /// <param name="argumentValue">Argument value to test.</param>
        /// <param name="argumentName">Name of the argument being tested (optional).</param>
        /// <exception cref="System.ArgumentNullException"><parameref name="argumentValue" /> is null.</exception>
        public static void ArgumentNotNull<T>(T argumentValue, string argumentName = "") where T : class
        {
            if (argumentValue != null)
            {
                return;
            }

            if (string.IsNullOrEmpty(argumentName))
            {
                throw new ArgumentNullException();
            }

            throw new ArgumentNullException(argumentName);
        }
    }
}