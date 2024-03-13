using AutoMapper;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Commands.Create;
using SFC.Player.Application.Models.Base;
using SFC.Player.Application.Features.Player.Common;

namespace SFC.Player.Application.Features.Player.Create;

public class CreatePlayerResponse :
    BaseErrorResponse, IMapFrom<CreatePlayerViewModel>
{
    public PlayerModel Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreatePlayerViewModel, CreatePlayerResponse>()
                                                   .IgnoreAllNonExisting();
}
