using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; }
        DbSet<Transaction> Transactions { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
