using System;
using System.Linq;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Models.Category;
using Application.Models.Common;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(IApplicationDbContext context) : base(context)
        {
        }

        protected override IQueryable<Category> ApplyFilterInternal(IQueryable<Category> query, Request request)
        {
            if (request is not GetCategoryRequest getCategoryRequest)
            {
                return query;
            }

            if (getCategoryRequest.Id != Guid.Empty)
            {
                query = query.Where(x => x.Id == getCategoryRequest.Id);
            }

            if (!string.IsNullOrEmpty(getCategoryRequest.Name))
            {
                query = query.Where(x => x.Name.Contains(getCategoryRequest.Name));
            }

            if (!string.IsNullOrEmpty(getCategoryRequest.Description))
            {
                query = query.Where(x => x.Description.Contains(getCategoryRequest.Description));
            }

            //if (getCategoryRequest.UserId != Guid.Empty)
            //{
            //    query = query.Where(x => x.UserId == getCategoryRequest.UserId);
            //}

            return query;
        }
    }
}