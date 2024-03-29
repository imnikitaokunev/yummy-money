﻿using System;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Transaction : Entity<long>
    {
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Category Category { get; set; }
        public User User { get; set; }
    }
}
