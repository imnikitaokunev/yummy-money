using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.Expense;
using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface IExpenseRepository : IRepository<Expense, long>
    {
        Task<List<Expense>> GetWithCategoryAsync(GetExpensesRequest request);
    }
}
