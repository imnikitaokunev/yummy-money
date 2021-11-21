using System;
using Application.Models.Common;

namespace Application.Models.Transaction
{
    public class GetTransactionsRequest : Request
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