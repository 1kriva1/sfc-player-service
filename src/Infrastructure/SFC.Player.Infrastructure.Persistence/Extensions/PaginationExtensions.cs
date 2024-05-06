using Microsoft.EntityFrameworkCore;

using SFC.Player.Application.Features.Common.Models;
using SFC.Player.Application.Features.Common.Models.Filters;
using SFC.Player.Application.Features.Common.Models.Paging;
using SFC.Player.Application.Features.Common.Models.Sorting;

namespace SFC.Player.Infrastructure.Persistence.Extensions;
public static class PaginationExtensions
{
    #region Public

    public static async Task<PagedList<T>> PaginateAsync<T>(this IQueryable<T> query, FindParameters<T> parameters)
    {
        return await query.PaginateAsync(parameters.Pagination, parameters.Sorting, parameters.Filters);
    }

    public static async Task<PagedList<T>> PaginateAsync<T>(this IQueryable<T> query, Pagination pagination)
    {
        int count = await query.CountAsync();

        List<T> items = await query.Skip((pagination.Page - 1) * pagination.Size).Take(pagination.Size).ToListAsync();

        return new PagedList<T>(items, count, pagination);
    }

    public static async Task<PagedList<T>> PaginateAsync<T>(this IQueryable<T> query, Pagination pagination, Sortings<T> sorts)
    {
        IQueryable<T> result = query.ApplySort(sorts);

        return await result.PaginateAsync(pagination);
    }

    public static async Task<PagedList<T>> PaginateAsync<T>(this IQueryable<T> query, Pagination pagination, Sortings<T> sorts, Filters<T> filters)
    {
        IQueryable<T> results = query.ApplyFilter(filters);

        return await results.PaginateAsync(pagination, sorts);
    }

    #endregion Public

    #region Private

    private static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, Filters<T> filters)
    {
        IQueryable<T> results = !filters.IsValid ? query : filters.FilterList.Aggregate(query, (current, filter) => current.Where(filter.Expression));

        return results;
    }

    private static IQueryable<T> ApplySort<T>(this IQueryable<T> query, Sortings<T> sorts)
    {
        if (!sorts.IsValid) return query;

        IEnumerable<Sorting<T, dynamic>> sorting = sorts.Get();

        return sorting != null ? Sortings<T>.ApplySort(query, sorting) : query;
    }

    #endregion Private
}
