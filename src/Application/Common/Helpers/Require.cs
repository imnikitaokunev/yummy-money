using System;

namespace Application.Common.Helpers
{
    public static class Require
    {
        /// <summary>
        /// Throws <see cref="System.ArgumentNullException" /> if the given argument is null.
        /// </summary>
        /// <param name="argumentValue">Argument value to test.</param>
        /// <param name="argumentName">Name of the argument being tested (optional).</param>
        /// <exception cref="System.ArgumentNullException"><parameref name="argumentValue" /> is null.</exception>
        public static void NotNull<T>(T argumentValue, string argumentName = null) where T : class
        {
            if (argumentValue != null)
            {
                return;
            }

            if (string.IsNullOrEmpty(argumentName))
            {
                #pragma warning disable CA2208 // Instantiate argument exceptions correctly
                throw new ArgumentNullException();
                #pragma warning restore CA2208 // Instantiate argument exceptions correctly
            }

            throw new ArgumentNullException(argumentName);
        }

        /// <summary>
        ///     Throws <see cref="System.ArgumentException" /> if the given argument does not satisfy the condition.
        /// </summary>
        /// <typeparam name="T">The type of the first argument.</typeparam>
        /// <param name="argumentValue">Argument value to test.</param>
        /// <param name="condition">Predicate function that describes the condition.</param>
        /// <param name="violationMessage">Message in case of violation of the condition (optional).</param>
        /// <param name="argumentName">Name of the argument being tested (optional).</param>
        /// <exception cref="System.ArgumentException">
        ///     <paramref name="argumentValue" /> does not satisfy <paramref name="condition" />.
        /// </exception>
        public static void Satisfies<T>(T argumentValue, Func<T, bool> condition, string violationMessage = "",
            string argumentName = "")
        {
            if (condition(argumentValue))
            {
                return;
            }

            if (string.IsNullOrEmpty(violationMessage))
            {
                throw new ArgumentException("Must satisfy the condition");
            }

            if (string.IsNullOrEmpty(argumentName))
            {
                throw new ArgumentException(violationMessage);
            }

            throw new ArgumentException(violationMessage, argumentName);
        }

        /// <summary>
        ///     Throws <see cref="System.ArgumentException" /> if the given string is null or empty.
        /// </summary>
        /// <param name="value">String value to test.</param>
        /// <param name="argumentName">Name of the argument being tested (optional).</param>
        /// <exception cref="System.ArgumentException">
        ///     <paramref name="value" /> is null or empty>.
        /// </exception>
        public static void NotNullOrEmpty(string value, string argumentName = "")
        {
            if (!string.IsNullOrEmpty(value))
            {
                return;
            }

            if (string.IsNullOrEmpty(argumentName))
            {
                throw new ArgumentException("Must be not null or empty");
            }

            throw new ArgumentException("Must be not null or empty", argumentName);
        }
    }
}
