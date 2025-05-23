using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace SFC.Player.Application.Features.Common.Models.Find.Filters;

public class Filters<T>
{
    private readonly List<Filter<T>> _filterList;

    public bool IsValid => _filterList.Any(f => f.Condition);

    public Collection<Filter<T>> FilterList => new(_filterList.Where(f => f.Condition).ToList());

    public Filters()
    {
        _filterList = [];
    }

    public Filters(IEnumerable<Filter<T>> filters)
    {
        _filterList = new List<Filter<T>>(filters);
    }

    public void Add(bool condition, Expression<Func<T, bool>> expression)
    {
        _filterList.Add(new Filter<T>
        {
            Condition = condition,
            Expression = expression
        });
    }
}
