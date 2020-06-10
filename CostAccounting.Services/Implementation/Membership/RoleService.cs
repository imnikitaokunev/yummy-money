using System.Collections.Generic;
using System.Linq;
using CostAccounting.Core.Models;
using CostAccounting.Core.Repositories.Membership;
using CostAccounting.Services.Interfaces.Membership;
using CostAccounting.Services.Mappers;
using CostAccounting.Services.Models.Role;

namespace CostAccounting.Services.Implementation.Membership
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository) => _repository = repository;

        public List<RoleModel> Get(RequestModel request)
        {
            var roles = _repository.Get(request).Select(x => x.ToModel()).ToList();
            return roles;
        }
    }
}
