using System.Linq.Expressions;

using SFC.Players.Application.Common.Enums;

namespace SFC.Players.Application.Features.Common.Models.Sorting;
public class Sorting<T, TKey>
{
    public bool Condition { get; set; }

    public Expression<Func<T, TKey>> Expression { get; set; } = default!;

    public SortingDirection Direction { get; set; }
}
