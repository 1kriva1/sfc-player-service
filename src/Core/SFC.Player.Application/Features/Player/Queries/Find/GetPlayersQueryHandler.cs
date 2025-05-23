using AutoMapper;

using MediatR;

using SFC.Player.Application.Features.Common.Dto.Pagination;
using SFC.Player.Application.Features.Common.Models.Find;
using SFC.Player.Application.Features.Common.Models.Find.Filters;
using SFC.Player.Application.Features.Common.Models.Find.Paging;
using SFC.Player.Application.Features.Common.Models.Find.Sorting;
using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;
using SFC.Player.Application.Features.Player.Queries.Find.Extensions;
using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Persistence.Repository.Player;
using SFC.Player.Domain.Events.Player;

namespace SFC.Player.Application.Features.Player.Queries.Find;
public class GetPlayersQueryHandler(
    IMapper mapper,
    IPlayerRepository playerRepository,
    IDateTimeService dateTimeService,
    IMediator mediator) : IRequestHandler<GetPlayersQuery, GetPlayersViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IPlayerRepository _playerRepository = playerRepository;
    private readonly IDateTimeService _dateTimeService = dateTimeService;
    private readonly IMediator _mediator = mediator;

    public async Task<GetPlayersViewModel> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Filter<PlayerEntity>> filters = request.Filter.BuildSearchFilters(_dateTimeService.DateNow);

        IEnumerable<Sorting<PlayerEntity, dynamic>> sorting = request.Sorting.BuildPlayerSearchSorting();

        FindParameters<PlayerEntity> parameters = new()
        {
            Pagination = _mapper.Map<Pagination>(request.Pagination),
            Filters = new Filters<PlayerEntity>(filters),
            Sorting = new Sortings<PlayerEntity>(sorting)
        };

        PagedList<PlayerEntity> pageList = await _playerRepository.FindAsync(parameters).ConfigureAwait(true);

        await PublishPlayersRetrievedEvent(pageList, request.Filter, cancellationToken).ConfigureAwait(false);

        return new GetPlayersViewModel
        {
            Items = _mapper.Map<IEnumerable<PlayerDto>>(pageList),
            Metadata = _mapper.Map<PageMetadataDto>(pageList)
        };
    }

    private Task PublishPlayersRetrievedEvent(IEnumerable<PlayerEntity> players, GetPlayersFilterDto filter,
        CancellationToken cancellationToken)
    {
        return players.Any() && (!string.IsNullOrEmpty(filter?.Profile?.General?.Name) || (filter?.Profile?.Football?.Positions?.Any() ?? false))
            ? _mediator.Publish(new PlayersRetrievedEvent(players), cancellationToken)
            : Task.CompletedTask;
    }
}
