using System;

namespace Application.Models.Expense
{
    public class CreateExpenseRequest
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}
