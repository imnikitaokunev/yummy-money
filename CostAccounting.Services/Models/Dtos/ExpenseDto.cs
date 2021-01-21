using System;

namespace CostAccounting.Services.Models.Dtos
{
    public class ExpenseDto
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public CategoryDto Category { get; set; }
    }
}
