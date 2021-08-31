using System;
using Application.Models.Category;

namespace Application.Models.Income
{
    public class IncomeWithCategoryDto
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public Guid UserId { get; set; }
    }
}
