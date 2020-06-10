using System.Collections.Generic;

namespace CostAccounting.Core.Models
{
    public abstract class RequestModel
    {
        public IReadOnlyCollection<string> Includes { get; set; }
    }
}
