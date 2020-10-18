using System;
using CostAccounting.Core.Entities.Membership;

namespace CostAccounting.Core.Entities.Core
{
    public class Income : Entity<long>
    {
        public const int DescriptionLength = 128;

        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Category Category { get; set; }
        public User User { get; set; }
    }
}