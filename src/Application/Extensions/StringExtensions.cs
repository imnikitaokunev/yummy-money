using Application.Common.Helpers;

namespace Application.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            Require.NotNullOrEmpty(str);

            if (str.Length == 1)
            {
                return str.ToLower();
            }

            return char.ToLowerInvariant(str[0]) + str[1..];
        }
    }
}
