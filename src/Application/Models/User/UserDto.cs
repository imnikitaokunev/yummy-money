using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Application.Models.User;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public List<IdentityRole> Roles { get; set; }
}
