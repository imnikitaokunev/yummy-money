using System;
using System.Linq;
using CostAccounting.Core.Entities.Membership;
using CostAccounting.Services.Models.User;
using Mapster;

namespace CostAccounting.Services
{
    public static class MapsterConfiguration
    {
        public static void Configure()
        {
            TypeAdapterConfig.GlobalSettings.ForType<User, UserModel>()
                .Ignore(src => src.PasswordHash, src => src.PasswordSalt)
                .Map(dest => dest, src => Convert.ToBase64String(src.Photo))
                .Map(dest => dest.Roles, src => src.Roles.Select(x => x.Role.Name));
            //.IgnoreNullValues(true);
        }
    }
}
