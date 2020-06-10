using System.Collections.Generic;
using System.Linq;
using CostAccounting.Core.Models;
using CostAccounting.Core.Repositories.Membership;
using CostAccounting.Services.Interfaces.Membership;
using CostAccounting.Services.Models.Role;
using Mapster;

namespace CostAccounting.Services.Implementation.Membership
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository) => _repository = repository;

        public List<RoleModel> Get(RequestModel request)
        {
            var roles = _repository.Get(request);
            return roles.Select(x => x.Adapt<RoleModel>()).ToList();
        }
    }
}
