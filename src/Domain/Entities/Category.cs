using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Category : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
