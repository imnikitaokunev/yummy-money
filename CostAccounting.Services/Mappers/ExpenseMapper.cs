using CostAccounting.Core.Entities;
using CostAccounting.Services.Models.Expense;

namespace CostAccounting.Services.Mappers
{
    public static class ExpenseMapper
    {
        public static ExpenseModel ToModel(this Expense entity) => new ExpenseModel
        {
            Id = entity.Id,
            CategoryId = entity.CategoryId,
            Amount = entity.Amount,
            Date = entity.Date,
            Description = entity.Description
        };

        public static Expense ToEntity(this ExpenseModel model) => new Expense
        {
            Id = model.Id,
            CategoryId = model.CategoryId,
            Amount = model.Amount,
            Date = model.Date,
            Description = model.Description
        };
    }
}
