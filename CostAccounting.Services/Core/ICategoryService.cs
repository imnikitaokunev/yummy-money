using System;
using System.Collections.Generic;
using CostAccounting.Core.Models.Core;
using CostAccounting.Services.Models.Category;
using CostAccounting.Services.Models.Error;

namespace CostAccounting.Services.Core
{
    public interface ICategoryService
    {
        List<CategoryModel> Get(CategoryRequestModel request);

        RepositoryResult Create(CategoryModel model);

        CategoryModel GetById(Guid id);

        RepositoryResult Update(CategoryModel model);

        RepositoryResult Delete(Guid id);
    }
}
