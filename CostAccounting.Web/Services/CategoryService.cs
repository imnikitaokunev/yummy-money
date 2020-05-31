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
            var categories = _repository.Get(request);
            return categories.Select(x => x.ToModel()).ToList();
        }

        public void Create(CategoryModel model)
        {
            model.Id = SqlServerFriendlyGuid.Generate();
            var entity = model.ToEntity();
            _repository.Create(entity);
            _repository.Save();
        }

        public CategoryModel GetById(Guid id)
        {
            var category = _repository.GetById(id);
            return category?.ToModel();
        }

        public void Update(CategoryModel model)
        {
            var entity = model.ToEntity();
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
