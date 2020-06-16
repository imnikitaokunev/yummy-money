using System;
using CostAccounting.Core.Entities.Security;

namespace CostAccounting.Services.Interfaces.Security
{
    public interface IRefreshTokenService
    {
        RefreshToken GetRefreshToken(Guid id);
        void Create(RefreshToken refreshToken);
        void Update(RefreshToken refreshToken);
    }
}
