using AutoMapper;

using SFC.Player.Api.Infrastructure.Models.Base;
using SFC.Player.Api.Infrastructure.Models.Player.General.Common;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Commands.Create;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Create;

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
