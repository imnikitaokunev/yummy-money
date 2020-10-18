using System.Collections.Generic;
using CostAccounting.Core.Entities.Core;
using CostAccounting.Core.Models.Core;
using CostAccounting.Services.Models.Dtos;
using CostAccounting.Services.Models.Error;

namespace CostAccounting.Services.Core
{
    public interface IIncomeService
    {
        IEnumerable<IncomeDto> Get(IncomeRequestModel request);

        RepositoryResult<IncomeDto> Create(IncomeDto model);

        IncomeDto GetById(long id);

        RepositoryResult<IncomeDto> Update(IncomeDto model);

        RepositoryResult<IncomeDto> Delete(long id);
    }
}
