using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Expense : Entity<long>
    {
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Category Category { get; set; }
        public User User { get; set; }
    }
}
