using System;
using System.Linq;
using System.Collections.Generic;
using CostAccounting.Core.Models;
using CostAccounting.Core.Repositories;
using CostAccounting.Services.Mappers;
using CostAccounting.Services.Models.Category;
using CostAccounting.Services.Services;
using CostAccounting.Shared;

namespace CostAccounting.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository) => _repository = repository;

        public List<CategoryModel> Get(CategoryRequestModel request)
        {
            var categories = _repository.GetAsync(request).Result;
            return categories.Select(x => x.ToModel()).ToList();
        }

        public CategoryModel Create(CategoryModel model)
        {
            model.Id = SqlServerFriendlyGuid.Generate();

            var entity = model.ToEntity();
            var isCreated = _repository.CreateAsync(entity).Result;

            return isCreated ? entity.ToModel() : null;
        }

        public CategoryModel GetById(Guid id)
        {
            var category = _repository.GetByIdAsync(id).Result;
            return category?.ToModel();
        }

        public bool Update(CategoryModel model)
        {
            var entity = model.ToEntity();
            var isUpdated = _repository.UpdateAsync(entity).Result;
            return isUpdated;
        }

        public bool Delete(Guid id)
        {
            var isDeleted = _repository.DeleteAsync(id).Result;
            return isDeleted;
        }
    }
}
