using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Find.Filters;

/// <summary>
/// Get players filter model.
/// </summary>
public class GetPlayersFilterModel : IMapTo<GetPlayersFilterDto>
{
    /// <summary>
    /// Profile filter model.
    /// </summary>
    public GetPlayersProfileFilterModel Profile { get; set; } = default!;

    /// <summary>
    /// Stats filter model.
    /// </summary>
    public GetPlayersStatsFilterModel Stats { get; set; } = default!;
}
