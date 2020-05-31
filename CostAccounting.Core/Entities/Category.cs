﻿using System;

namespace CostAccounting.Core.Entities
{
    public class Category : Entity<Guid>
    {
        public const int NameLength = 16;
        public const int DescriptionLength = 128;

        public string Name { get; set; }
        public string Description { get; set; }
    }
}