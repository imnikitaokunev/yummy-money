using System;
using System.Linq;
using CostAccounting.Shared;
using System.Collections.Generic;
using CostAccounting.Core.Entities.Core;
using CostAccounting.Core.Models.Core;
using CostAccounting.Core.Repositories.Core;
using CostAccounting.Services.Interfaces.Core;
using CostAccounting.Services.Models.Category;
using Mapster;

namespace CostAccounting.Services.Implementation.Core
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository) => _repository = repository;

        public List<CategoryModel> Get(CategoryRequestModel request)
        {
            var categories = _repository.Get(request);
            return categories.Select(x => x.Adapt<CategoryModel>()).ToList();
        }

        public void Create(CategoryModel model)
        {
            model.Id = SqlServerFriendlyGuid.Generate();
            var entity = model.Adapt<Category>();
            _repository.Create(entity);
            _repository.Save();
        }

        public CategoryModel GetById(Guid id)
        {
            var category = _repository.GetById(id);
            return category?.Adapt<CategoryModel>();
        }

        public void Update(CategoryModel model)
        {
            var entity = model.Adapt<Category>();
            _repository.Update(entity);
            _repository.Save();
        }

        public void Delete(Guid id)
        {
            var entity = _repository.GetById(id);
            _repository.Delete(entity);
            _repository.Save();
        }
    }
}
