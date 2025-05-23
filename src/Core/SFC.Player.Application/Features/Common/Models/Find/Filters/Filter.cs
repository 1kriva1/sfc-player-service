using System.Linq.Expressions;

namespace SFC.Player.Application.Features.Common.Models.Find.Filters;
public class Filter<T>
{
    public bool Condition { get; set; }

    public Expression<Func<T, bool>> Expression { get; set; } = default!;
}
