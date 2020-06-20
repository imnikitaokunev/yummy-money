using System.Collections.Generic;

namespace CostAccounting.Services.Models.Error
{
    public class RepositoryResult
    {
        public bool Success { get; set; }
        // TODO: Think about name.
        public object Target { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
