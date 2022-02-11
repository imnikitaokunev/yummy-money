using System;
using Application.Models.Category;
using Domain.Enums;

namespace Application.Models.Transaction
{
    public class TransactionDto
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public CategoryDto Category { get; set; }
        public Guid UserId { get; set; }
    }
}
