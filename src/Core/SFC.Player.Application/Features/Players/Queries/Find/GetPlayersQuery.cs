using AutoMapper;

using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Base;
using SFC.Player.Application.Features.Players.Queries.Find.Dto.Filters;
using SFC.Player.Application.Models.Players.Find;

namespace SFC.Player.Application.Features.Players.Queries.Find;
public class GetPlayersQuery :
    BasePaginationRequest<GetPlayersViewModel, GetPlayersFilterDto>,
    IMapFrom<GetPlayersRequest>
{
    public override RequestId RequestId { get => RequestId.GetPlayers; }

    public void Mapping(Profile profile) => profile.CreateMap<GetPlayersRequest, GetPlayersQuery>()
                                                   .IgnoreAllNonExisting();
}
