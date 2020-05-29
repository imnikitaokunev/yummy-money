using System.Collections.Generic;
using System.Threading.Tasks;
using CostAccounting.Core.Entities;
using CostAccounting.Core.Models;
using CostAccounting.Core.Repositories;

namespace CostAccounting.Data.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        public async Task<List<Category>> GetAsync(RequestModel request) => throw new System.NotImplementedException();

        public async Task<bool> CreateAsync(Expense entity) => throw new System.NotImplementedException();

        public async Task<Expense> GetByIdAsync(long id) => throw new System.NotImplementedException();

        public async Task<bool> UpdateAsync(Expense entity) => throw new System.NotImplementedException();

        public async Task<bool> DeleteAsync(long id) => throw new System.NotImplementedException();
    }
}
