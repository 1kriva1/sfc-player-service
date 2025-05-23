using AutoMapper;

using SFC.Player.Api.Infrastructure.Models.Base;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Queries.Find;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Api.Infrastructure.Models.Player.General.Find.Filters;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Find;

/// <summary>
/// **Get** players request.
/// </summary>
public class GetPlayersRequest : BasePaginationRequest<GetPlayersFilterModel>, IMapTo<GetPlayersQuery>
{
    public void Mapping(Profile profile) => profile.CreateMap<GetPlayersRequest, GetPlayersQuery>()
                                                   .IgnoreAllNonExisting();
}
