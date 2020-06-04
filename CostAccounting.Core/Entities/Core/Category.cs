using System;

namespace CostAccounting.Core.Entities.Core
{
    public class Category : Entity<Guid>
    {
        public const int NameLength = 32;
        public const int DescriptionLength = 128;

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
