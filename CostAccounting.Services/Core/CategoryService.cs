using System;
using System.Collections.Generic;
using CostAccounting.Core.Entities.Core;
using CostAccounting.Core.Exceptions;
using CostAccounting.Core.Models.Core;
using CostAccounting.Core.Repositories.Core;
using CostAccounting.Services.Models.Error;

namespace CostAccounting.Services.Core
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository) => _repository = repository;

        public IEnumerable<Category> Get(CategoryRequestModel request) => _repository.Get(request);

        public RepositoryResult<Category> Create(Category category)
        {
            var result = new RepositoryResult<Category>();

            try
            {
                _repository.Create(category);
                _repository.Save();
            }
            catch (RepositoryException ex)
            {
                result.Errors = ex.Errors;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string> {ex.GetBaseException().Message};
                return result;
            }

            result.Success = true;
            result.Target = category;

            return result;
        }

        public Category GetById(Guid id) => _repository.GetById(id);

        public RepositoryResult<Category> Update(Category category)
        {
            var result = new RepositoryResult<Category>();

            try
            {
                _repository.Update(category);
                _repository.Save();
            }
            catch (RepositoryException ex)
            {
                result.Errors = ex.Errors;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string> {ex.GetBaseException().Message};
                return result;
            }

            result.Success = true;
            result.Target = category;

            return result;
        }

        public RepositoryResult<Category> Delete(Guid id)
        {
            var result = new RepositoryResult<Category>();
            var category = _repository.GetById(id);

            // TODO: Check if category is null?

            try
            {
                _repository.Delete(category);
                _repository.Save();
            }
            catch (RepositoryException ex)
            {
                result.Errors = ex.Errors;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors = new List<string> {ex.GetBaseException().Message};
                return result;
            }

            result.Success = true;
            result.Target = category;

            return result;
        }
    }
}