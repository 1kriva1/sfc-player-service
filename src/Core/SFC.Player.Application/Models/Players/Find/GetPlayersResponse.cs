using AutoMapper;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Queries.Find;
using SFC.Player.Application.Models.Base;
using SFC.Player.Application.Models.Players.Find.Result;

namespace SFC.Player.Application.Models.Players.Find;

/// <summary>
/// **Get** players response model.
/// </summary>
public class GetPlayersResponse : BaseListResponse<PlayerModel>, IMapFrom<GetPlayersViewModel>
{
    public void Mapping(Profile profile) => profile.CreateMap<GetPlayersViewModel, GetPlayersResponse>()
                                                   .IgnoreAllNonExisting();
}
