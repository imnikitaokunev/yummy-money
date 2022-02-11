using System.Linq;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Models.Common;
using Application.Models.Transaction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class TransactionRepository : Repository<Transaction, long>, ITransactionRepository
    {
        public TransactionRepository(IApplicationDbContext context) : base(context)
        {
        }

        protected override IQueryable<Transaction> ApplyFilterInternal(IQueryable<Transaction> query, Request request)
        {
            if (request is not GetTransactionsRequest getTransactionsRequest)
            {
                return query;
            }

            if (getTransactionsRequest.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == getTransactionsRequest.CategoryId);
            }

            if (getTransactionsRequest.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == getTransactionsRequest.UserId);
            }

            if (getTransactionsRequest.MinAmount.HasValue)
            {
                query = query.Where(x => x.Amount >= getTransactionsRequest.MinAmount);
            }

            if (getTransactionsRequest.MaxAmount.HasValue)
            {
                query = query.Where(x => x.Amount <= getTransactionsRequest.MaxAmount);
            }

            if (getTransactionsRequest.Type.HasValue)
            {
                query = query.Where(x => x.Type == getTransactionsRequest.Type.Value);
            }

            if (getTransactionsRequest.StartDate.HasValue)
            {
                query = query.Where(x => x.Date >= getTransactionsRequest.StartDate);
            }

            if (getTransactionsRequest.EndDate.HasValue)
            {
                query = query.Where(x => x.Date < getTransactionsRequest.EndDate);
            }

            if (!string.IsNullOrEmpty(getTransactionsRequest.Description))
            {
                query = query.Where(x => x.Description.Contains(getTransactionsRequest.Description));
            }

            return query;
        }

        protected override IQueryable<Transaction> Include(IQueryable<Transaction> query)
        {
            return query.Include(x => x.Category);
        }
    }
}
