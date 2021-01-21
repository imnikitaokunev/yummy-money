using System;

namespace CostAccounting.Core.Models.Security
{
    public class RefreshTokenRequestModel : RequestModel
    {
        public Guid Id { get; set; }
    }
}
