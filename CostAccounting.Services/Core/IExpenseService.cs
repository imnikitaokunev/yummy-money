using System.Collections.Generic;
using CostAccounting.Core.Entities.Core;
using CostAccounting.Core.Models.Core;
using CostAccounting.Services.Models.Dtos;
using CostAccounting.Services.Models.Error;

namespace CostAccounting.Services.Core
{
    public interface IExpenseService
    {
        IEnumerable<ExpenseDto> Get(ExpenseRequestModel request);
        
        RepositoryResult<Expense> Create(Expense model);

        Expense GetById(long id);

        RepositoryResult<Expense> Update(Expense model);

        RepositoryResult<Expense> Delete(long id);
    }
}
