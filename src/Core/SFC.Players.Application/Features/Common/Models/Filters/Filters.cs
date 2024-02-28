using System.Linq.Expressions;

namespace SFC.Players.Application.Features.Common.Models.Filters;

public class Filters<T>
{
    private readonly List<Filter<T>> _filterList;

    public bool IsValid => _filterList.Any(f => f.Condition);

    public List<Filter<T>> FilterList => _filterList.Where(f => f.Condition).ToList();

    public Filters()
    {
        _filterList = new List<Filter<T>>();
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
