using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Find.Filters;

/// <summary>
/// Get players **profile filter** model.
/// </summary>
public class GetPlayersProfileFilterModel : IMapTo<GetPlayersProfileFilterDto>
{
    /// <summary>
    /// General profile.
    /// </summary>
    public GetPlayersGeneralProfileFilterModel General { get; set; } = default!;

    /// <summary>
    /// Football profile.
    /// </summary>
    public GetPlayersFootballProfileFilterModel Football { get; set; } = default!;
}
