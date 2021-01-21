using System.Collections.Generic;

namespace CostAccounting.Services.Models.Auth
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
