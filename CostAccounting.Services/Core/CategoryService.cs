using System;
using System.Collections.Generic;
using System.Linq;
using CostAccounting.Core.Entities.Core;
using CostAccounting.Core.Exceptions;
using CostAccounting.Core.Models.Core;
using CostAccounting.Core.Repositories.Core;
using CostAccounting.Services.Models.Category;
using CostAccounting.Services.Models.Error;
using Mapster;

namespace CostAccounting.Services.Core
{
    // TODO: Подогнать сервисы под реальные запросы
    
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository) => _repository = repository;

        public List<CategoryModel> Get(CategoryRequestModel request)
        {
            var categories = _repository.Get(request);
            return categories.Select(x => x.Adapt<CategoryModel>()).ToList();
        }

        public RepositoryResult Create(CategoryModel model)
        {
            // TODO: Find more optimal way to generate ids?

            var entity = model.Adapt<Category>();
            var response = new RepositoryResult();

            try
            {
                _repository.Create(entity);
                _repository.Save();
            }
            catch (Exception ex)
            {
                response.Errors = new List<string> { ex.GetBaseException().Message };
                return response;
            }

            response.Success = true;
            response.Target = entity.Adapt<CategoryModel>();

            return response;
        }

        public CategoryModel GetById(Guid id)
        {
            var category = _repository.GetById(id);
            return category?.Adapt<CategoryModel>();
        }

        public RepositoryResult Update(CategoryModel model)
        {
            var entity = model.Adapt<Category>();
            var response = new RepositoryResult();

            try
            {
                _repository.Update(entity);
                _repository.Save();
            }
            catch (Exception ex)
            {
                response.Errors = new List<string> { ex.GetBaseException().Message };
                return response;
            }

            response.Success = true;
            response.Target = entity.Adapt<CategoryModel>();

            return response;
        }

        public RepositoryResult Delete(Guid id)
        {
            var entity = _repository.GetById(id);
            var response = new RepositoryResult();

            try
            {
                _repository.Delete(entity);
                _repository.Save();
            }
            catch (RepositoryException ex)
            {
                response.Errors = ex.Errors;
                return response;
            }
            catch (Exception ex)
            {
                response.Errors = new List<string> { ex.GetBaseException().Message };
                return response;
            }

            response.Success = true;
            //response.Target = entity.Adapt<CategoryModel>();

            return response;
        }
    }
}
