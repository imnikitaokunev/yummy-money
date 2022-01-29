using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Models.Common;

public class PaginatedList<T>
{
    public List<T> Items { get; }
    public int TotalCount { get; }
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalPages { get; }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    public PaginatedList(List<T> items, int totalCount, int pageIndex, int pageSize)
    {
        Items = items;
        PageIndex = pageIndex;
        PageSize = pageSize < totalCount ? pageSize : totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        TotalCount = totalCount;
    }

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> query, int pageIndex, int pageSize)
    {
        var count = await query.CountAsync();
        var items = await ApplyPagination(query, pageIndex, pageSize).ToListAsync();

        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }

    public static IQueryable<T> ApplyPagination(IQueryable<T> query, int pageIndex, int pageSize) =>
        query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
}
