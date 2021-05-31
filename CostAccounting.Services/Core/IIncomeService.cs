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

        RepositoryResult<Income> Create(Income model);

        Income GetById(long id);

        RepositoryResult<Income> Update(Income model);

        RepositoryResult<Income> Delete(long id);
    }
}
