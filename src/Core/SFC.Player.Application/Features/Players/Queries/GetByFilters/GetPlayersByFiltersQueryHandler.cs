using AutoMapper;

using MediatR;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Features.Common.Dto.Pagination;
using SFC.Player.Application.Features.Common.Models.Filters;
using SFC.Player.Application.Features.Common.Models.Paging;
using SFC.Player.Application.Features.Common.Models.Sorting;
using SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Filters;
using SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Result;
using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Events;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Player.Queries.GetByFilters;
public record GetPlayersByFiltersQueryHandler(IMapper Mapper, IPlayerRepository PlayerRepository,
    IDateTimeService DateTimeService, IUriService UriService,
    IMediator Mediator)
    : IRequestHandler<GetPlayersByFiltersQuery, GetPlayersByFiltersViewModel>
{
    public async Task<GetPlayersByFiltersViewModel> Handle(GetPlayersByFiltersQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<Filter<PlayerEntity>> filters = query.Filter.BuildSearchFilters(DateTimeService.DateNow);

        IEnumerable<Sorting<PlayerEntity, dynamic>> sorting = query.Sorting.BuildSearchSorting();

        PageParameters<PlayerEntity> parameters = new()
        {
            Pagination = Mapper.Map<Pagination>(query.Pagination),
            Filters = new Filters<PlayerEntity>(filters),
            Sorting = new Sortings<PlayerEntity>(sorting)
        };

        PagedList<PlayerEntity> pageList = await PlayerRepository.GetPageAsync(parameters);

        await PublishPlayersByFiltersEvent(pageList, query.Filter, cancellationToken);

        return new GetPlayersByFiltersViewModel
        {
            Items = Mapper.Map<IEnumerable<PlayerByFiltersDto>>(pageList),
            Metadata = Mapper.Map<PageMetadataDto>(pageList)
                             .SetLinks(UriService, query.QueryString, query.Route)
        };
    }

    private Task PublishPlayersByFiltersEvent(IEnumerable<PlayerEntity> players, GetPlayersByFiltersFilterDto filter,
        CancellationToken cancellationToken)
    {
        if (players.Any() && (!string.IsNullOrEmpty(filter?.Profile?.General?.Name) || (filter?.Profile?.Football?.Positions?.Any() ?? false)))
        {
            return Mediator.Publish(new PlayersByFiltersEvent(players), cancellationToken);
        }

        return Task.CompletedTask;
    }
}
