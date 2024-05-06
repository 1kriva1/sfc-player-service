using AutoMapper;

using MediatR;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Features.Common.Dto.Pagination;
using SFC.Player.Application.Features.Common.Models;
using SFC.Player.Application.Features.Common.Models.Filters;
using SFC.Player.Application.Features.Common.Models.Paging;
using SFC.Player.Application.Features.Common.Models.Sorting;
using SFC.Player.Application.Features.Players.Queries.Find.Dto.Filters;
using SFC.Player.Application.Features.Players.Queries.Find.Dto.Result;
using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Events;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Players.Queries.Find;
public record GetPlayersQueryHandler(IMapper Mapper, IPlayerRepository PlayerRepository,
    IDateTimeService DateTimeService, IUriService UriService,
    IMediator Mediator)
    : IRequestHandler<GetPlayersQuery, GetPlayersViewModel>
{
    public async Task<GetPlayersViewModel> Handle(GetPlayersQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<Filter<PlayerEntity>> filters = query.Filter.BuildSearchFilters(DateTimeService.DateNow);

        IEnumerable<Sorting<PlayerEntity, dynamic>> sorting = query.Sorting.BuildSearchSorting();

        FindParameters<PlayerEntity> parameters = new()
        {
            Pagination = Mapper.Map<Pagination>(query.Pagination),
            Filters = new Filters<PlayerEntity>(filters),
            Sorting = new Sortings<PlayerEntity>(sorting)
        };

        PagedList<PlayerEntity> pageList = await PlayerRepository.FindAsync(parameters);

        await PublishGetPlayersEvent(pageList, query.Filter, cancellationToken);

        return new GetPlayersViewModel
        {
            Items = Mapper.Map<IEnumerable<PlayerDto>>(pageList),
            Metadata = Mapper.Map<PageMetadataDto>(pageList)
                             .SetLinks(UriService, query.QueryString, query.Route)
        };
    }

    private Task PublishGetPlayersEvent(IEnumerable<PlayerEntity> players, GetPlayersFilterDto filter,
        CancellationToken cancellationToken)
    {
        if (players.Any() && (!string.IsNullOrEmpty(filter?.Profile?.General?.Name) || (filter?.Profile?.Football?.Positions?.Any() ?? false)))
        {
            return Mediator.Publish(new GetPlayersEvent(players), cancellationToken);
        }

        return Task.CompletedTask;
    }
}
