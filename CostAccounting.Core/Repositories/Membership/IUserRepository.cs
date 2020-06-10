using System;
using CostAccounting.Core.Entities.Membership;

namespace CostAccounting.Core.Repositories.Membership
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}