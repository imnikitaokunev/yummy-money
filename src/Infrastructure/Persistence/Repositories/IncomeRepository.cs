using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Models.Income;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class IncomeRepository : Repository<Income, long>, IIncomeRepository
    {
        public IncomeRepository(IApplicationDbContext context) : base(context)
        {
        }

        protected override DbSet<Income> DbSet => Context.Incomes;

        protected override IQueryable<Income> ApplyFilterInternal(IQueryable<Income> query, Request request)
        {
            if (request is not GetIncomesRequest getIncomesRequest)
            {
                return query;
            }

            if (getIncomesRequest.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == getIncomesRequest.CategoryId);
            }

            if (getIncomesRequest.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == getIncomesRequest.UserId);
            }

            if (getIncomesRequest.MinAmount.HasValue)
            {
                query = query.Where(x => x.Amount >= getIncomesRequest.MinAmount);
            }

            if (getIncomesRequest.MaxAmount.HasValue)
            {
                query = query.Where(x => x.Amount <= getIncomesRequest.MaxAmount);
            }

            if (getIncomesRequest.StartDate.HasValue)
            {
                query = query.Where(x => x.Date >= getIncomesRequest.StartDate);
            }

            if (getIncomesRequest.EndDate.HasValue)
            {
                query = query.Where(x => x.Date < getIncomesRequest.EndDate);
            }

            if (!string.IsNullOrEmpty(getIncomesRequest.Description))
            {
                query = query.Where(x => x.Description.Contains(getIncomesRequest.Description));
            }

            return query;
        }

        public async Task<List<Income>> GetWithCategoryAsync(GetIncomesRequest request)
        {
            var filteredQuery = ApplyFilter(DbSet, request);
            var sortedFilteredQuery = ApplySort(filteredQuery, request.SortBy, request.SortType);
            return await sortedFilteredQuery.Include(x => x.Category).AsNoTracking().ToListAsync();
        }
    }
}
