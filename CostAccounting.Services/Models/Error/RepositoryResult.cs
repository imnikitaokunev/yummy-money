using System.Collections.Generic;

namespace CostAccounting.Services.Models.Error
{
    public class RepositoryResult<T>
    {
        public bool Success { get; set; }
        // TODO: Think about name.
        public T Target { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
