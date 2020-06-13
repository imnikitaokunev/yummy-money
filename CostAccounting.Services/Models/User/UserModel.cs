using System;
using System.Collections.Generic;

namespace CostAccounting.Services.Models.User
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredAt { get; set; }
        //public byte[] Photo { get; set; }
        public string Photo { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
