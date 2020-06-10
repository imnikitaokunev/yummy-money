using CostAccounting.Core.Entities.Membership;
using CostAccounting.Services.Models.Role;

namespace CostAccounting.Services.Mappers
{
    public static class RoleMapper
    {
        public static RoleModel ToModel(this Role entity) => new RoleModel
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}
