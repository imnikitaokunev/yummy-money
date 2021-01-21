using System;

namespace CostAccounting.Core.Models.Membership
{
    public class UserRequestModel : RequestModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
