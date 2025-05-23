using SFC.Player.Application.Features.Common.Dto.Common;

namespace SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;
public class GetPlayersGeneralProfileFilterDto
{
    public string? Name { get; set; }

    public string? City { get; set; }

    public IEnumerable<string> Tags { get; set; } = [];

    public RangeLimitDto<short?> Years { get; set; } = default!;

    public GetPlayersAvailabilityLimitDto Availability { get; set; } = default!;

    public bool? FreePlay { get; set; }

    public bool? HasPhoto { get; set; }
}
