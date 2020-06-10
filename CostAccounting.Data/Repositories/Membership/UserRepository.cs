using System;
using System.Linq;
using CostAccounting.Core.Entities.Membership;
using CostAccounting.Core.Models;
using CostAccounting.Core.Repositories.Membership;
using CostAccounting.Shared;

namespace CostAccounting.Data.Repositories.Membership
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(CostAccountingContext context) : base(context)
        {
        }

        protected override IQueryable<User> ApplyFilterInternal(IQueryable<User> query, RequestModel request)
        {
            Expect.ArgumentNotNull(query, nameof(query));

            return query;
        }
    }
}
