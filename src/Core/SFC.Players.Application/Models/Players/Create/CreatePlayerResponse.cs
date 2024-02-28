using AutoMapper;

using SFC.Players.Application.Common.Extensions;
using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Commands.Create;
using SFC.Players.Application.Models.Base;
using SFC.Players.Application.Models.Players.Common;

namespace SFC.Players.Application.Models.Players.Create;

public class CreatePlayerResponse :
    BaseErrorResponse, IMapFrom<CreatePlayerViewModel>
{
    public PlayerModel Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreatePlayerViewModel, CreatePlayerResponse>()
                                                   .IgnoreAllNonExisting();
}
