using System.Collections.Generic;

namespace CostAccounting.Services.Models.Error
{
    public class ValidationErrorResponse
    {
        public List<ValidationErrorModel> Errors { get; set; } = new List<ValidationErrorModel>();
    }
}
