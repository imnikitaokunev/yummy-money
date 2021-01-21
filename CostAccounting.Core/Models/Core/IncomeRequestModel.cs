using System;

namespace CostAccounting.Core.Models.Core
{
    public class IncomeRequestModel : RequestModel
    {
        public Guid? CategoryId { get; set; }
        public Guid? UserId { get; set; }

        public decimal? MinimalAmount { get; set; }
        public decimal? MaximalAmount { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Description { get; set; }
    }
}
