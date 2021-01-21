using System.Linq;
using CostAccounting.Core.Entities.Core;
using CostAccounting.Core.Models;
using CostAccounting.Core.Models.Core;
using CostAccounting.Core.Repositories.Core;
using CostAccounting.Shared;

namespace CostAccounting.Data.EntityFramework.Repositories.Core
{
    public class ExpenseRepository : Repository<Expense, long>, IExpenseRepository
    {
        public ExpenseRepository(CostAccountingContext context) : base(context)
        {
        }

        protected override IQueryable<Expense> ApplyFilterInternal(IQueryable<Expense> query, RequestModel requestModel)
        {
            Expect.ArgumentNotNull(query, nameof(query));

            if (!(requestModel is ExpenseRequestModel request))
            {
                return query;
            }

            if (request.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == request.CategoryId);
            }

            if (request.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == request.UserId);
            }

            if (request.MinimalAmount.HasValue)
            {
                query = query.Where(x => x.Amount >= request.MinimalAmount);
            }

            if (request.MaximalAmount.HasValue)
            {
                query = query.Where(x => x.Amount <= request.MaximalAmount);
            }

            if (request.StartDate.HasValue)
            {
                query = query.Where(x => x.Date >= request.StartDate);
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(x => x.Date <= request.EndDate);
            }

            if (!string.IsNullOrEmpty(request.Description))
            {
                query = query.Where(x => x.Description.Contains(request.Description));
            }

            return query;
        }
    }
}
