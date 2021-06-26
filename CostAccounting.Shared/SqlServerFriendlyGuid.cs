using System;
using System.Security.Cryptography;

namespace CostAccounting.Shared
{
    public static class SqlServerFriendlyGuid
    {
        public static Guid Generate()
        {
            using var provider = new RNGCryptoServiceProvider();

            var bytes = new byte[16];
            provider.GetBytes(bytes);

            return new Guid(bytes);
        }
    }
}