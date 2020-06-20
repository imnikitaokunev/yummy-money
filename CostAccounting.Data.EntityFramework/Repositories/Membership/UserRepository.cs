using System;
using System.Linq;
using CostAccounting.Core.Entities.Membership;
using CostAccounting.Core.Models;
using CostAccounting.Core.Models.Membership;
using CostAccounting.Core.Repositories.Membership;
using CostAccounting.Shared;

namespace CostAccounting.Data.EntityFramework.Repositories.Membership
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(CostAccountingContext context) : base(context)
        {
        }

        protected override IQueryable<User> ApplyFilterInternal(IQueryable<User> query, RequestModel requestModel)
        {
            Expect.ArgumentNotNull(query, nameof(query));

            if (!(requestModel is UserRequestModel request))
            {
                return query;
            }

            if (request.Id != Guid.Empty)
            {
                query = query.Where(x => x.Id == request.Id);
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(x => x.Email.Contains(request.Email));
            }

            if (!string.IsNullOrEmpty(request.Username))
            {
                query = query.Where(x => x.Username.Contains(request.Username));
            }

            return query;
        }
    }
}
