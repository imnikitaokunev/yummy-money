using System.Linq;
using System.Collections.Generic;
using CostAccounting.Services.Mappers;
using CostAccounting.Core.Models.Core;
using CostAccounting.Core.Repositories.Core;
using CostAccounting.Services.Interfaces.Core;
using CostAccounting.Services.Models.Expense;

namespace CostAccounting.Services.Implementation.Core
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repository;

        public ExpenseService(IExpenseRepository repository) => _repository = repository;

        public List<ExpenseModel> Get(ExpenseRequestModel request)
        {
            var expenses = _repository.Get(request);
            return expenses.Select(x => x.ToModel()).ToList();
        }

        public ExpenseModel Create(ExpenseModel model)
        {
            var entity = model.ToEntity();
            _repository.Create(entity);
            _repository.Save();
            return entity.ToModel();
        }

        public ExpenseModel GetById(long id)
        {
            var expense = _repository.GetById(id);
            return expense?.ToModel();
        }

        public void Update(ExpenseModel model)
        {
            var entity = model.ToEntity();
            _repository.Update(entity);
            _repository.Save();
        }

        public void Delete(long id)
        {
            var entity = _repository.GetById(id);
            _repository.Delete(entity);
            _repository.Save();
        }
    }
}
