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
        public static void ArgumentSatisfies<T>(T argumentValue, Func<T, bool> condition, string violationMessage = "",
            string argumentName = "")
        {
            if (condition(argumentValue))
            {
                return;
            }

            if (string.IsNullOrEmpty(violationMessage))
            {
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(argumentName))
            {
                throw new ArgumentException(violationMessage);
            }

            throw new ArgumentException(violationMessage, argumentName);
        }
    }
}