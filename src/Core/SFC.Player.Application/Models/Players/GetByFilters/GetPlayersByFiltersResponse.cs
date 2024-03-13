using AutoMapper;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Queries.Get;
using SFC.Player.Application.Features.Player.Queries.GetByFilters;
using SFC.Player.Application.Models.Base;
using SFC.Player.Application.Features.Player.Get;
using SFC.Player.Application.Features.Player.GetByFilters.Result;

namespace SFC.Player.Application.Features.Player.GetByFilters;
public class GetPlayersByFiltersResponse : BaseListResponse<PlayerByFiltersModel>, IMapFrom<GetPlayersByFiltersViewModel>
{
    public void Mapping(Profile profile) => profile.CreateMap<GetPlayersByFiltersViewModel, GetPlayersByFiltersResponse>()
                                                   .IgnoreAllNonExisting();
}
