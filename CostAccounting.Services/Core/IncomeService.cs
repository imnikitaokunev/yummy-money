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
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _repository;

        public IncomeService(IIncomeRepository repository) => _repository = repository;

        public IEnumerable<IncomeDto> Get(IncomeRequestModel request) => _repository.Get(request).Select(x => x.Adapt<IncomeDto>());

        public RepositoryResult<Income> Create(Income expense)
        {
            var result = new RepositoryResult<Income>();

            try
            {
                _repository.Create(expense);
                _repository.Save();
            }
            catch (RepositoryException ex)
            {
                result.Errors = ex.Errors;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string> { ex.GetBaseException().Message };
                return result;
            }

            result.Success = true;
            result.Target = expense;

            return result;
        }

        // TODO: Catch errors from repository.
        public Income GetById(long id) => _repository.GetById(id);

        public RepositoryResult<Income> Update(Income expense)
        {
            var result = new RepositoryResult<Income>();

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
                result.Errors = new List<string> { ex.GetBaseException().Message };
                return result;
            }

            result.Success = true;
            result.Target = expense;

            return result;
        }

        public RepositoryResult<Income> Delete(long id)
        {
            var result = new RepositoryResult<Income>();
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
                result.Errors = new List<string> { ex.GetBaseException().Message };
                return result;
            }

            result.Success = true;
            result.Target = expense;

            return result;
        }
    }
}
