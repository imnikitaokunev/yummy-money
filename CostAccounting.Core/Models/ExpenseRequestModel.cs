using System;

namespace CostAccounting.Core.Models
{
    public class ExpenseRequestModel
    {
        public Guid CategoryId { get; set; }
        
        public decimal MinimalAmount { get; set; }
        public decimal MaximalAmount { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Description { get; set; }
    }
}