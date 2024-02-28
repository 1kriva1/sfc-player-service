using System.Linq.Expressions;

namespace SFC.Players.Application.Features.Common.Models.Filters;
public class Filter<T>
{
    public bool Condition { get; set; }

    public Expression<Func<T, bool>> Expression { get; set; } = default!;
}
