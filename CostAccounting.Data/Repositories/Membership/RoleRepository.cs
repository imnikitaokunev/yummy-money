using System.Linq;
using CostAccounting.Core.Entities.Membership;
using CostAccounting.Core.Models;
using CostAccounting.Core.Repositories.Membership;
using CostAccounting.Shared;

namespace CostAccounting.Data.EntityFramework.Repositories.Membership
{
    public class RoleRepository : Repository<Role, int>, IRoleRepository
    {
        public RoleRepository(CostAccountingContext context) : base(context)
        {
        }

        protected override IQueryable<Role> ApplyFilterInternal(IQueryable<Role> query, RequestModel request)
        {
            Expect.ArgumentNotNull(query, nameof(query));

            return query;
        }
    }
}