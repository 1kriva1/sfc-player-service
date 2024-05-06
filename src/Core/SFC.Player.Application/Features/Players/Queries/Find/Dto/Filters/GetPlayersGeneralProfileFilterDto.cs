using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Models.Players.Find.Filters;

namespace SFC.Player.Application.Features.Players.Queries.Find.Dto.Filters;
public class GetPlayersGeneralProfileFilterDto : IMapFrom<GetPlayersGeneralProfileFilterModel>
{
    public string? Name { get; set; }

    public string? City { get; set; }

    public IEnumerable<string> Tags { get; set; } = Array.Empty<string>();

    public RangeLimitDto<short?> Years { get; set; } = default!;

    public GetPlayersAvailabilityLimitDto Availability { get; set; } = default!;

    public bool? FreePlay { get; set; }

    public bool? HasPhoto { get; set; }
}
