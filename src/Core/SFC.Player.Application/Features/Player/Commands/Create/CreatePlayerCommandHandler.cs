using AutoMapper;

using MediatR;

using SFC.Player.Application.Interfaces.Persistence.Repository.Player;
using SFC.Player.Domain.Events.Player;

namespace SFC.Player.Application.Features.Player.Commands.Create;

public class CreatePlayerCommandHandler(IMapper mapper, IPlayerRepository playerRepository)
    : IRequestHandler<CreatePlayerCommand, CreatePlayerViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IPlayerRepository _playerRepository = playerRepository;

    public async Task<CreatePlayerViewModel> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        PlayerEntity player = _mapper.Map<PlayerEntity>(request.Player);

        player.AddDomainEvent(new PlayerCreatedEvent(player));

        await _playerRepository.AddAsync(player).ConfigureAwait(true);

        return _mapper.Map<CreatePlayerViewModel>(player);
    }
}
