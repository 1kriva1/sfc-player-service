using System.Linq.Expressions;

using SFC.Player.Application.Common.Enums;

namespace SFC.Player.Application.Features.Common.Models.Find.Sorting;

public class Sortings<T>
{
    private readonly List<Sorting<T, dynamic>> _sortList;

    public bool IsValid => _sortList.Any(s => s.Condition);

    public IEnumerable<Sorting<T, dynamic>> Get() => _sortList.Where(s => s.Condition);

    public Sortings()
    {
        _sortList = [];
    }

    public Sortings(IEnumerable<Sorting<T, dynamic>> sorting)
    {
        _sortList = new List<Sorting<T, dynamic>>(sorting);
    }

    public void Add<TKey>(bool condition, Expression<Func<T, dynamic>> expression, SortingDirection direction = SortingDirection.Ascending)
    {
        Append(condition, expression, direction);
    }

    public IQueryable<T> ApplySort<TKey>(IQueryable<T> query, IEnumerable<Sorting<T, TKey>> sorting)
    {
#pragma warning disable CA1851 // Possible multiple enumerations of 'IEnumerable' collection
        Sorting<T, TKey>? main = sorting.FirstOrDefault();
#pragma warning restore CA1851 // Possible multiple enumerations of 'IEnumerable' collection

        if (main != null)
        {
            IOrderedQueryable<T> orderedQuery = main.Direction == SortingDirection.Ascending
               ? query.OrderBy(main.Expression)
               : query.OrderByDescending(main.Expression);

#pragma warning disable CA1851 // Possible multiple enumerations of 'IEnumerable' collection
            IEnumerable<Sorting<T, TKey>> secondary = sorting.Skip(1);
#pragma warning restore CA1851 // Possible multiple enumerations of 'IEnumerable' collection

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