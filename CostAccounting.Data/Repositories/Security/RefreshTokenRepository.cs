using System;
using System.Linq;
using CostAccounting.Core.Entities.Security;
using CostAccounting.Core.Models;
using CostAccounting.Core.Models.Security;
using CostAccounting.Core.Repositories.Security;
using CostAccounting.Shared;

namespace CostAccounting.Data.EntityFramework.Repositories.Security
{
    public class RefreshTokenRepository : Repository<RefreshToken, Guid>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(CostAccountingContext context) : base(context)
        {
        }

        protected override IQueryable<RefreshToken> ApplyFilterInternal(IQueryable<RefreshToken> query, RequestModel requestModel)
        {
            Expect.ArgumentNotNull(query, nameof(query));

            if (!(requestModel is RefreshTokenRequestModel request))
            {
                return query;
            }

            if (request.Id != Guid.Empty)
            {
                query = query.Where(x => x.Id == request.Id);
            }

            return query;
        }
    }
}
