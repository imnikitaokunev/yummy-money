using System.Collections.Generic;
using CostAccounting.Core.Entities;
using CostAccounting.Core.Models;
using CostAccounting.Core.Repositories;

namespace CostAccounting.Data.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        public List<Expense> Get(RequestModel request) => throw new System.NotImplementedException();

        public void Create(Expense entity) => throw new System.NotImplementedException();

        public Expense GetById(long id) => throw new System.NotImplementedException();

        public void Update(Expense entity) => throw new System.NotImplementedException();

        public void Delete(Expense expense) => throw new System.NotImplementedException();

        public void Save() => throw new System.NotImplementedException();
    }
}
