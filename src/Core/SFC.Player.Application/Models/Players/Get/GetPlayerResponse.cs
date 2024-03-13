using AutoMapper;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Queries.Get;
using SFC.Player.Application.Models.Base;
using SFC.Player.Application.Features.Player.Common;

namespace SFC.Player.Application.Features.Player.Get;

public class GetPlayerResponse :
    BaseErrorResponse, IMapFrom<GetPlayerViewModel>
{
    public PlayerModel Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<GetPlayerViewModel, GetPlayerResponse>()
                                                   .IgnoreAllNonExisting();
}
