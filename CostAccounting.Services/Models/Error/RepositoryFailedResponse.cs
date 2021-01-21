using System.Collections.Generic;

namespace CostAccounting.Services.Models.Error
{
    public class RepositoryFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
