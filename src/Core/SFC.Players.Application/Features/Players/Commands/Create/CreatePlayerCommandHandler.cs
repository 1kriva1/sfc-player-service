using AutoMapper;

using MediatR;

using SFC.Players.Application.Common.Extensions;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Events;

namespace SFC.Players.Application.Features.Players.Commands.Create;

public record CreatePlayerCommandHandler(IMapper Mapper, IPlayerRepository PlayerRepository)
    : IRequestHandler<CreatePlayerCommand, CreatePlayerViewModel>
{
    public async Task<CreatePlayerViewModel> Handle(CreatePlayerCommand command, CancellationToken cancellationToken)
    {
        Player player = Mapper.Map<Player>(command.Player)
                              .SetUser(command.UserId);

        player.AddDomainEvent(new PlayerCreatedEvent(player));

        await PlayerRepository.AddAsync(player);

        return Mapper.Map<CreatePlayerViewModel>(player);
    }
}
