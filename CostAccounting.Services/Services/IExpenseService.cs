using System.Collections.Generic;
using CostAccounting.Core.Models;
using CostAccounting.Services.Models.Expense;

namespace CostAccounting.Services.Services
{
    public interface IExpenseService
    {
        List<ExpenseModel> Get(ExpenseRequestModel request);

        ExpenseModel Create(ExpenseModel model);

        ExpenseModel GetById(long id);

        void Update(ExpenseModel model);

        void Delete(long id);
    }
}
