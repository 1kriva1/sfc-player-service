using AutoMapper;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Commands.Create;
using SFC.Player.Application.Models.Base;
using SFC.Player.Application.Models.Players.Common;

namespace SFC.Player.Application.Models.Players.Create;

/// <summary>
/// **Create** player response model.
/// </summary>
public class CreatePlayerResponse :
    BaseErrorResponse, IMapFrom<CreatePlayerViewModel>
{
    /// <summary>
    /// Player model.
    /// </summary>
    public PlayerModel Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreatePlayerViewModel, CreatePlayerResponse>()
                                                   .IgnoreAllNonExisting();
}
