using System;

namespace CostAccounting.Services.Models.Auth
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
