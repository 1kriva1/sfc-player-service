using AutoMapper;

using SFC.Players.Application.Common.Enums;
using SFC.Players.Application.Common.Extensions;
using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Common.Base;
using SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Filters;
using SFC.Players.Application.Models.Players.GetByFilters;

namespace SFC.Players.Application.Features.Players.Queries.GetByFilters;
public class GetPlayersByFiltersQuery :
    BasePaginationRequest<GetPlayersByFiltersViewModel, GetPlayersByFiltersFilterDto>,
    IMapFrom<GetPlayersByFiltersRequest>
{
    public override RequestId RequestId { get => RequestId.GetPlayersByFilters; }

    public void Mapping(Profile profile) => profile.CreateMap<GetPlayersByFiltersRequest, GetPlayersByFiltersQuery>()
                                                   .IgnoreAllNonExisting();
}
