using System;

namespace Application.Models
{
    public class CreateUserRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        //public DateTime RegisteredAt { get; set; }
        //public byte[] Photo { get; set; }
    }
}
