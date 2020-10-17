using System;
using System.Collections.Generic;
using CostAccounting.Core.Entities.Core;
using CostAccounting.Core.Models.Core;
using CostAccounting.Services.Models.Error;

namespace CostAccounting.Services.Core
{
    public interface ICategoryService
    {
        IEnumerable<Category> Get(CategoryRequestModel request);

        RepositoryResult<Category> Create(Category model);

        Category GetById(Guid id);

        RepositoryResult<Category> Update(Category model);

        RepositoryResult<Category> Delete(Guid id);
    }
}
