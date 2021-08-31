using System;

namespace Application.Models.Income
{
    public class CreateIncomeRequest
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}
