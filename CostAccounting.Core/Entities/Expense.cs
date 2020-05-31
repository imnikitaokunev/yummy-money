using System;

namespace CostAccounting.Core.Entities
{
    public class Expense : Entity<long>
    {
        public const int DescriptionLength = 128;

        public Guid CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Category Category { get; set; }
    }
}
