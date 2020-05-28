using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CostAccounting.Core.Entities;
using CostAccounting.Core.Models;
using CostAccounting.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CostAccounting.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CostAccountingContext _context;

        public CategoryRepository(CostAccountingContext context) => _context = context;

        public async Task<List<Category>> GetAsync(RequestModel requestModel)
        {
            var request = requestModel as CategoryRequestModel;
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(request?.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            if (!string.IsNullOrEmpty(request?.Description))
            {
                query = query.Where(x => x.Description.Contains(request.Description));
            }

            return await query.ToListAsync();
        }

        public async Task<bool> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _context.Categories.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = await GetByIdAsync(id);
            _context.Categories.Remove(category);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
