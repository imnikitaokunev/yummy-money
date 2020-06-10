using System;

namespace CostAccounting.Core.Entities.Membership
{
    public class UserRole : Entity
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }

        public Role Role { get; }
    }
}
