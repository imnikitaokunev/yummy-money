using System;

namespace CostAccounting.Services.Models.Expense
{
    public class ExpenseModel
    {
        public long Id { get; set; }
        public Guid CategoryId { get; set; }
        //  Todo: Is this property needed here?
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }


        // TODO: Category model?
    }
}
