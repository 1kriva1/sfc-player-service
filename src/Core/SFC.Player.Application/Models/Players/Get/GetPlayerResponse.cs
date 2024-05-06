using AutoMapper;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Queries.Get;
using SFC.Player.Application.Models.Base;
using SFC.Player.Application.Models.Players.Common;

namespace SFC.Player.Application.Models.Players.Get;

/// <summary>
/// **Get** player response model.
/// </summary>
public class GetPlayerResponse :
    BaseErrorResponse, IMapFrom<GetPlayerViewModel>
{
    /// <summary>
    /// Player model.
    /// </summary>
    public PlayerModel Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<GetPlayerViewModel, GetPlayerResponse>()
                                                   .IgnoreAllNonExisting();
}
