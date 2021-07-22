using System;
using System.Collections.Generic;
using System.Linq;
using CostAccounting.Core.Entities.Core;
using CostAccounting.Core.Exceptions;
using CostAccounting.Core.Models.Core;
using CostAccounting.Core.Repositories.Core;
using CostAccounting.Services.Models.Dtos;
using CostAccounting.Services.Models.Error;
using Mapster;

namespace CostAccounting.Services.Core
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repository;

        public ExpenseService(IExpenseRepository repository) => _repository = repository;

        public IEnumerable<ExpenseDto> Get(ExpenseRequestModel request) => _repository.Get(request).Select(x => x.Adapt<ExpenseDto>());

        public RepositoryResult<Expense> Create(Expense expense)
        {
            var result = new RepositoryResult<Expense>();

            //try
            //{
                _repository.Create(expense);
                _repository.Save();
            //}
            //catch (RepositoryException ex)
            //{
            //    result.Errors = ex.Errors;
            //    return result;
            //}
            //catch (Exception ex)
            //{
            //    result.Errors = new List<string> {ex.GetBaseException().Message};
            //    return result;
            //}

            result.Success = true;
            result.Target = expense;

            return result;
        }

        // TODO: Catch errors from repository.
        public Expense GetById(long id) => _repository.GetById(id);

        public RepositoryResult<Expense> Update(Expense expense)
        {
            var result = new RepositoryResult<Expense>();

            try
            {
                _repository.Update(expense);
                _repository.Save();
            }
            catch (RepositoryException ex)
            {
                result.Errors = ex.Errors;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string> {ex.GetBaseException().Message};
                return result;
            }

            result.Success = true;
            result.Target = expense;

            return result;
        }

        public RepositoryResult<Expense> Delete(long id)
        {
            var result = new RepositoryResult<Expense>();
            var expense = _repository.GetById(id);

            try
            {
                _repository.Delete(expense);
                _repository.Save();
            }
            catch (RepositoryException ex)
            {
                result.Errors = ex.Errors;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string> {ex.GetBaseException().Message};
                return result;
            }

            result.Success = true;
            result.Target = expense;

            return result;
        }
    }
}