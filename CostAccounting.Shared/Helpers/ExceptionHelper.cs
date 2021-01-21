using System;
using System.Collections.Generic;

namespace CostAccounting.Shared.Helpers
{
    public static class ExceptionHelper
    {
        public static IEnumerable<Exception> GetInnerExceptions(this Exception ex)
        {
            var innerException = ex;

            do
            {
                yield return innerException;
                innerException = innerException.InnerException;
            }
            while (innerException != null);
        }
    }
}
