using AutoMapper;

using SFC.Player.Api.Infrastructure.Models.Base;
using SFC.Player.Api.Infrastructure.Models.Player.General.Common;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Queries.Find;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Find;

/// <summary>
/// **Get** players response model.
/// </summary>
public class GetPlayersResponse : BaseListResponse<PlayerModel>, IMapFrom<GetPlayersViewModel>
{
    public void Mapping(Profile profile) => profile.CreateMap<GetPlayersViewModel, GetPlayersResponse>()
                                                   .IgnoreAllNonExisting();
}
