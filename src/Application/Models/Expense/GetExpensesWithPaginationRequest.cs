using System;
using Application.Common.Models;

namespace Application.Models.Expense
{
    public class GetExpensesWithPaginationRequest : PaginationRequest
    {
        public Guid? CategoryId { get; set; }
        public Guid? UserId { get; set; }

        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Description { get; set; }
    }
}
