using AutoMapper;

using SFC.Players.Application.Common.Extensions;
using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Queries.Get;
using SFC.Players.Application.Features.Players.Queries.GetByFilters;
using SFC.Players.Application.Models.Base;
using SFC.Players.Application.Models.Players.Get;
using SFC.Players.Application.Models.Players.GetByFilters.Result;

namespace SFC.Players.Application.Models.Players.GetByFilters;
public class GetPlayersByFiltersResponse : BaseListResponse<PlayerByFiltersModel>, IMapFrom<GetPlayersByFiltersViewModel>
{
    public void Mapping(Profile profile) => profile.CreateMap<GetPlayersByFiltersViewModel, GetPlayersByFiltersResponse>()
                                                   .IgnoreAllNonExisting();
}
