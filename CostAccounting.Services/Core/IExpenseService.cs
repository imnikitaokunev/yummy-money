using System.Collections.Generic;
using CostAccounting.Core.Models.Core;
using CostAccounting.Services.Models.Expense;

namespace CostAccounting.Services.Core
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
