using System.Linq.Expressions;

using SFC.Player.Application.Features.Common.Dto.Common;
using SFC.Player.Application.Features.Common.Models.Find.Sorting;
using SFC.Player.Domain.Common;

namespace SFC.Player.Application.Features.Common.Extensions;
public static class SortingExtensions
{
    public static IEnumerable<Sorting<TEntity, dynamic>> BuildSearchSorting<TEntity>(this IEnumerable<SortingDto> sorting,
        Func<string, Expression<Func<TEntity, dynamic>>?> buildSortingExpression)
    {
        List<Sorting<TEntity, dynamic>> result = [];

        sorting ??= [];

        foreach (SortingDto sort in sorting)
        {
            Expression<Func<TEntity, dynamic>>? expression = buildSortingExpression(sort.Name);

#pragma warning disable CA1508 // Avoid dead conditional code
            if (expression is not null)
            {
                result.Add(new()
                {
                    Condition = true,
                    Direction = sort.Direction,
                    Expression = expression
                });
            }
#pragma warning restore CA1508 // Avoid dead conditional code
        }

        return result;
    }
}
