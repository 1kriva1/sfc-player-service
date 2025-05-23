using SFC.Player.Application.Features.Common.Dto.Common;

namespace SFC.Player.Application.Features.Common.Extensions;
public static class FiltersExtensions
{
    public static bool BuildLimitFromCondition(this RangeLimitDto<short?>? limit, Tuple<int, int> rangeLimit)
    {
        return (limit?.From.HasValue ?? false)
            && !(limit.From == rangeLimit.Item1 && limit.To == rangeLimit.Item2);
    }

    public static bool BuildLimitToCondition(this RangeLimitDto<short?>? limit, Tuple<int, int> rangeLimit)
    {
        return (limit?.To.HasValue ?? false)
            && !(limit.From == rangeLimit.Item1 && limit.To == rangeLimit.Item2);
    }
}
