using System.Linq;
using System.Threading.Tasks;
using Application.Common.Models;

namespace Application.Extensions
{
    public static class QueryableExtensions
    {
        public static Task<PaginatedList<T>> PaginatedListAsync<T>(this IQueryable<T> query, int pageNumber,
            int pageSize)
            => PaginatedList<T>.CreateAsync(query, pageNumber, pageSize);
    }
}