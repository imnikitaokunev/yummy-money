using System;
using System.Collections.Generic;

namespace CostAccounting.Services.Models.Auth
{
    public class AuthenticationResult
    {
        // Todo: can be moved to .Core?
        public string Token { get; set; }
        public Guid RefreshToken { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
