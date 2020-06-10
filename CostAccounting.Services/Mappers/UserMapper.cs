using System;
using System.Linq;
using CostAccounting.Core.Entities.Membership;
using CostAccounting.Services.Models.User;

namespace CostAccounting.Services.Mappers
{
    public static class UserMapper
    {
        public static UserModel ToModel(this User entity) => new UserModel
        {
            Id = entity.Id,
            Email = entity.Email,
            PasswordHash = entity.PasswordHash,
            PasswordSalt = entity.PasswordSalt,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            RegisteredAt = entity.RegisteredAt,
            Photo = Convert.ToBase64String(entity.Photo),
            Roles = entity.Roles?.Select(x => x.Role?.Name)
        };
    }
}
