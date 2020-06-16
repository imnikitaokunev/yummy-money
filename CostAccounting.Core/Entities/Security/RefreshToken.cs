using System;
using CostAccounting.Core.Entities.Membership;

namespace CostAccounting.Core.Entities.Security
{
    public class RefreshToken : Entity<Guid>
    {
        public string JwtId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsUsed { get; set; }
        public bool IsInvalidated { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}