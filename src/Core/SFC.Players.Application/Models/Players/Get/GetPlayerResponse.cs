using AutoMapper;

using SFC.Players.Application.Common.Extensions;
using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Common.Models;
using SFC.Players.Application.Features.Players.Queries.Get;
using SFC.Players.Application.Models.Players.Common.Models;

namespace SFC.Players.Application.Models.Players.Get;

public class GetPlayerResponse :
    BaseErrorResponse, IMapFrom<GetPlayerViewModel>
{
    public PlayerModel Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<GetPlayerViewModel, GetPlayerResponse>()
                                                   .IgnoreAllNonExisting();
}
