using System.Collections.Generic;
using CostAccounting.Core.Models;

namespace CostAccounting.Services.Services
{
    public interface IService<TModel, in TKey> where TKey : struct
    {
        List<TModel> Get(CategoryRequestModel request);

        TModel Create(TModel model);

        TModel GetById(TKey id);

        bool Update(TModel model);

        bool Delete(TKey id);
    }
}
