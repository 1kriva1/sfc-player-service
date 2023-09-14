using AutoMapper;

using SFC.Players.Application.Common.Extensions;
using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Common.Models;
using SFC.Players.Application.Features.Players.Commands.Create;
using SFC.Players.Application.Models.Players.Common.Models;

namespace SFC.Players.Application.Models.Players.Create;

public class CreatePlayerResponse :
    BaseErrorResponse, IMapFrom<CreatePlayerViewModel>
{
    public PlayerModel Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreatePlayerViewModel, CreatePlayerResponse>()
                                                   .IgnoreAllNonExisting();
}
