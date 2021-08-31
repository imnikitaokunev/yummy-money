using System;
using System.Collections.Generic;
using Application.Models.Role;

namespace Application.Models.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredAt { get; set; }
        public byte[] Photo { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}
