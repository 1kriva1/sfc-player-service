using AutoMapper;

using MediatR;

using SFC.Players.Application.Common.Extensions;
using SFC.Players.Application.Features.Common.Dto.Pagination;
using SFC.Players.Application.Features.Common.Models.Filters;
using SFC.Players.Application.Features.Common.Models.Paging;
using SFC.Players.Application.Features.Common.Models.Sorting;
using SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Result;
using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Queries.GetByFilters;
public record GetPlayersByFiltersQueryHandler(IMapper Mapper, IPlayerRepository PlayerRepository, IDateTimeService DateTimeService, IUriService UriService)
    : IRequestHandler<GetPlayersByFiltersQuery, GetPlayersByFiltersViewModel>
{
    public async Task<GetPlayersByFiltersViewModel> Handle(GetPlayersByFiltersQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<Filter<Player>> filters = query.Filter.BuildSearchFilters(DateTimeService.DateNow);

        IEnumerable<Sorting<Player, dynamic>> sorting = query.Sorting.BuildSearchSorting();

        PageParameters<Player> parameters = new()
        {
            Pagination = Mapper.Map<Pagination>(query.Pagination),
            Filters = new Filters<Player>(filters),
            Sorting = new Sortings<Player>(sorting)
        };

        PagedList<Player> pageList = await PlayerRepository.GetPageAsync(parameters);

        return new GetPlayersByFiltersViewModel
        {
            Items = Mapper.Map<IEnumerable<PlayerByFiltersDto>>(pageList),
            Metadata = Mapper.Map<PageMetadataDto>(pageList)
                             .SetLinks(UriService, query.QueryString, query.Route)
        };
    }
}
