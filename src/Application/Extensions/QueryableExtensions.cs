using Application.Models.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class QueryableExtensions
    {
        public static Task<PaginatedList<T>> PaginatedListAsync<T>(this IQueryable<T> query, int pageNumber,
            int pageSize)
            => PaginatedList<T>.CreateAsync(query, pageNumber, pageSize);
    }
}