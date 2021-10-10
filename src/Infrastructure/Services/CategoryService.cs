using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Models.Category;
using Application.Models.Common;
using Domain.Entities;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAsync(GetCategoryRequest request)
        {
            var categories = await _categoryRepository.GetAsync(request);
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<PaginatedList<CategoryDto>> GetPagedResponseAsync(GetCategoryWithPaginationRequest request)
        {
            var categories = await _categoryRepository.GetPagedResponseAsync(request);
            return _mapper.Map<PaginatedList<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> AddAsync(CreateCategoryRequest category)
        {
            var entity = _mapper.Map<Category>(category);
            var created = await _categoryRepository.AddAsync(entity);
            return _mapper.Map<CategoryDto>(created);
        }

        public async Task UpdateAsync(CategoryDto category)
        {
            var entity = _mapper.Map<Category>(category);
            await _categoryRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
}