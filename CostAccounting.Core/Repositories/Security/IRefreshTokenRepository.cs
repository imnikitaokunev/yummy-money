using System;
using CostAccounting.Core.Entities.Security;

namespace CostAccounting.Core.Repositories.Security
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken, Guid>
    {
    }
}
