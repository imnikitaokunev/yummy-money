using CostAccounting.Core.Entities;

namespace CostAccounting.Core.Repositories
{
    public interface IExpenseRepository : IRepository<Expense, long>
    {
    }
}