using AutoMapper;

using MediatR;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities;
using SFC.Player.Domain.Events;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Player.Commands.Create;
public record CreatePlayerCommandHandler(IMapper Mapper, IPlayerRepository PlayerRepository)
    : IRequestHandler<CreatePlayerCommand, CreatePlayerViewModel>
{
    public async Task<CreatePlayerViewModel> Handle(CreatePlayerCommand command, CancellationToken cancellationToken)
    {
        PlayerEntity player = Mapper.Map<PlayerEntity>(command.Player)
                                    .SetUser(command.UserId);

        player.AddDomainEvent(new PlayerCreatedEvent(player));

        await PlayerRepository.AddAsync(player);

        return Mapper.Map<CreatePlayerViewModel>(player);
    }
}
