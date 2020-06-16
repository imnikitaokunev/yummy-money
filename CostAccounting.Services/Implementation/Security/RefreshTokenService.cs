using System;
using System.Linq;
using CostAccounting.Core.Entities.Security;
using CostAccounting.Core.Models.Security;
using CostAccounting.Core.Repositories.Security;
using CostAccounting.Services.Interfaces.Security;

namespace CostAccounting.Services.Implementation.Security
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository) => _refreshTokenRepository = refreshTokenRepository;

        public RefreshToken GetRefreshToken(Guid id)
        {
            var request = new RefreshTokenRequestModel {Id = id};
            var refreshToken = _refreshTokenRepository.Get(request).FirstOrDefault();

            return refreshToken;
        }

        public void Create(RefreshToken refreshToken)
        {
            _refreshTokenRepository.Create(refreshToken);
            _refreshTokenRepository.Save();
        }

        public void Update(RefreshToken refreshToken)
        {
            _refreshTokenRepository.Update(refreshToken);
            _refreshTokenRepository.Save();
        }
    }
}
