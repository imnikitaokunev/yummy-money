using System;
using System.Collections.Generic;
using System.Linq;
using CostAccounting.Core.Models.Core;
using CostAccounting.Core.Repositories.Core;
using CostAccounting.Services.Models.Dtos;
using CostAccounting.Services.Models.Error;
using Mapster;

namespace CostAccounting.Services.Core
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeService(IIncomeRepository incomeRepository) => _incomeRepository = incomeRepository;

        public IEnumerable<IncomeDto> Get(IncomeRequestModel request)
        {
            return _incomeRepository.Get(request).Select(x => x.Adapt<IncomeDto>());
        }

        public RepositoryResult<IncomeDto> Create(IncomeDto model) => throw new NotImplementedException();

        public IncomeDto GetById(long id) => throw new NotImplementedException();

        public RepositoryResult<IncomeDto> Update(IncomeDto model) => throw new NotImplementedException();

        public RepositoryResult<IncomeDto> Delete(long id) => throw new NotImplementedException();
    }
}
