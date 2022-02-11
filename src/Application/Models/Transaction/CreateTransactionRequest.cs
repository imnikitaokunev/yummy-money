using System;
using Domain.Enums;

namespace Application.Models.Transaction
{
    public class CreateTransactionRequest
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }  
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}
