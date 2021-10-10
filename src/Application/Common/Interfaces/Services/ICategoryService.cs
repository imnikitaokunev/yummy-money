using Application.Models.Category;
using Application.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAsync(GetCategoryRequest request);
        Task<PaginatedList<CategoryDto>> GetPagedResponseAsync(GetCategoryWithPaginationRequest request);
        Task<CategoryDto> GetByIdAsync(Guid id);
        Task<CategoryDto> AddAsync(CreateCategoryRequest category);
        Task UpdateAsync(CategoryDto category);
        Task DeleteAsync(Guid id);
    }
}
