using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ExpenseRepository : Repository<Expense, long>, IExpenseRepository
    {
        protected override DbSet<Expense> DbSet => Context.Expenses;

        public ExpenseRepository(IApplicationDbContext context) : base(context)
        {
        }

        protected override IQueryable<Expense> ApplyFilterInternal(IQueryable<Expense> query, Request request)
        {
            if (request is not GetExpensesRequest getExpensesRequest)
            {
                return query;
            }

            if (getExpensesRequest.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == getExpensesRequest.CategoryId);
            }

            if (getExpensesRequest.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == getExpensesRequest.UserId);
            }

            if (getExpensesRequest.MinAmount.HasValue)
            {
                query = query.Where(x => x.Amount >= getExpensesRequest.MinAmount);
            }

            if (getExpensesRequest.MaxAmount.HasValue)
            {
                query = query.Where(x => x.Amount <= getExpensesRequest.MaxAmount);
            }

            if (getExpensesRequest.StartDate.HasValue)
            {
                query = query.Where(x => x.Date >= getExpensesRequest.StartDate);
            }

            if (getExpensesRequest.EndDate.HasValue)
            {
                query = query.Where(x => x.Date <= getExpensesRequest.EndDate);
            }

            if (!string.IsNullOrEmpty(getExpensesRequest.Description))
            {
                query = query.Where(x => x.Description.Contains(getExpensesRequest.Description));
            }

            return query;
        }

        public async Task<List<Expense>> GetWithCategoryAsync(GetExpensesRequest request)
        {
            var filteredQuery = ApplyFilter(DbSet, request);
            var sortedAndFilteredQuery = ApplySort(filteredQuery, request.SortBy, request.SortType);
            return await sortedAndFilteredQuery.Include(x => x.Category).AsNoTracking().ToListAsync();
        }
    }
}