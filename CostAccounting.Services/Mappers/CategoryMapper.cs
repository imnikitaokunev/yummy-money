using CostAccounting.Core.Entities;
using CostAccounting.Services.Models.Category;

namespace CostAccounting.Services.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryModel ToModel(this Category entity) => new CategoryModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description
        };

        public static Category ToEntity(this CategoryModel model) => new Category
        {
            //Id = model.Id,
            Name = model.Name,
            Description = model.Description
        };
    }
}
