using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Application.Models.Identity
{
    public class AuthenticateResponse
    {
        public bool Succeeded { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }
        public string Token { get; set; }
    }
}
