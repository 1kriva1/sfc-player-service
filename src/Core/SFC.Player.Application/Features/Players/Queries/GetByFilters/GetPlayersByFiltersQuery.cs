using AutoMapper;

using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Base;
using SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Filters;
using SFC.Player.Application.Features.Player.GetByFilters;

namespace SFC.Player.Application.Features.Player.Queries.GetByFilters;
public class GetPlayersByFiltersQuery :
    BasePaginationRequest<GetPlayersByFiltersViewModel, GetPlayersByFiltersFilterDto>,
    IMapFrom<GetPlayersByFiltersRequest>
{
    public override RequestId RequestId { get => RequestId.GetPlayersByFilters; }

    public void Mapping(Profile profile) => profile.CreateMap<GetPlayersByFiltersRequest, GetPlayersByFiltersQuery>()
                                                   .IgnoreAllNonExisting();
}
