using System.Linq.Expressions;

using SFC.Players.Application.Common.Enums;

namespace SFC.Players.Application.Features.Common.Models.Sorting;

public class Sortings<T>
{
    private readonly List<Sorting<T, dynamic>> _sortList;

    public bool IsValid => _sortList.Any(s => s.Condition);

    public IEnumerable<Sorting<T, dynamic>> Get() => _sortList.Where(s => s.Condition);

    public Sortings()
    {
        _sortList = new List<Sorting<T, dynamic>>();
    }

    public Sortings(IEnumerable<Sorting<T, dynamic>> sorting)
    {
        _sortList = new List<Sorting<T, dynamic>>(sorting);
    }

    public void Add<TKey>(bool condition, Expression<Func<T, dynamic>> expression, SortingDirection direction = SortingDirection.Ascending)
    {
        Append(condition, expression, direction);
    }

    public static IQueryable<T> ApplySort<TKey>(IQueryable<T> query, IEnumerable<Sorting<T, TKey>> sorting)
    {
        Sorting<T, TKey>? main = sorting.FirstOrDefault();

        if (main != null)
        {
            IOrderedQueryable<T> orderedQuery = main.Direction == SortingDirection.Ascending
               ? query.OrderBy(main.Expression)
               : query.OrderByDescending(main.Expression);

            IEnumerable<Sorting<T, TKey>> secondary = sorting.Skip(1);

            foreach (Sorting<T, TKey> sort in secondary)
            {
                orderedQuery = sort.Direction == SortingDirection.Ascending
                    ? orderedQuery.ThenBy(sort.Expression)
                    : orderedQuery.ThenByDescending(main.Expression);
            }

            return orderedQuery;
        }

        return query;
    }

    private void Append(bool condition, Expression<Func<T, dynamic>> expression, SortingDirection direction)
    {
        _sortList.Add(new Sorting<T, dynamic>
        {
            Condition = condition,
            Expression = expression,
            Direction = direction
        });
    }
}
