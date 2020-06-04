using System;
using System.Linq;
using CostAccounting.Core.Models;
using CostAccounting.Core.Entities.Core;
using CostAccounting.Core.Models.Core;
using CostAccounting.Core.Repositories.Core;

namespace CostAccounting.Data.Repositories.Core
{
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(CostAccountingContext context) : base(context)
        {
        }

        protected override IQueryable<Category> ApplyFilter(RequestModel requestModel)
        {
            var query = DbSet.AsQueryable();

            if (!(requestModel is CategoryRequestModel request))
            {
                return query;
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            if (!string.IsNullOrEmpty(request.Description))
            {
                query = query.Where(x => x.Description.Contains(request.Description));
            }

            return query;
        }
    }
}