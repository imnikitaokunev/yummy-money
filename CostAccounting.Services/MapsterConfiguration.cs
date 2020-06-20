using System;
using System.Linq;
using CostAccounting.Core.Entities.Core;
using CostAccounting.Core.Entities.Membership;
using CostAccounting.Services.Models.Category;
using CostAccounting.Services.Models.User;
using CostAccounting.Shared;
using Mapster;

namespace CostAccounting.Services
{
    public static class MapsterConfiguration
    {
        public static void Configure()
        {
            TypeAdapterConfig.GlobalSettings.ForType<CategoryModel, Category>()
                .Map(dest => dest.Id, src => SqlServerFriendlyGuid.Generate());

            TypeAdapterConfig.GlobalSettings.ForType<User, UserModel>()
                .Map(dest => dest, src => Convert.ToBase64String(src.Photo))
                .Map(dest => dest.Roles, src => src.Roles.Select(x => x.Role.Name));
        }
    }
}
