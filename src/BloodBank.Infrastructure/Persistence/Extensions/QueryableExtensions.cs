using BloodBank.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Infrastructure.Persistence.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize) where T : class
    {
        var totalRecords = await query.CountAsync();
        var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSize);

        var data = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<T>(data, pageNumber, pageSize, totalRecords, totalPages);
    }
}
