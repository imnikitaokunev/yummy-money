using System.Collections.Generic;
using CostAccounting.Core.Models;
using CostAccounting.Services.Models.Role;

namespace CostAccounting.Services.Membership
{
    public interface IRoleService
    {
        List<RoleModel> Get(RequestModel request);
    }
}
