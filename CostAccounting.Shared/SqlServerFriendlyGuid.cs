using System;
using System.Runtime.InteropServices;

namespace CostAccounting.Shared
{
    public static class SqlServerFriendlyGuid
    {
        public static Guid Generate()
        {
            const int rpcSOk = 0;
            var status = UuidCreateSequential(out var guid);
            if (status != rpcSOk)
            {
                return Guid.NewGuid();
            }

            var s = guid.ToByteArray();

            return new Guid(new[]
                {s[3], s[2], s[1], s[0], s[5], s[4], s[7], s[6], s[8], s[9], s[10], s[11], s[12], s[13], s[14], s[15]});
        }

        [DllImport("rpcrt4.dll", SetLastError = true)]
        private static extern int UuidCreateSequential(out Guid guid);
    }
}
