using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface ITransactionRepository : IRepository<Transaction, long>
{
}
