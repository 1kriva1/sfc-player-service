using AutoMapper;

using MassTransit;

using SFC.Player.Application.Interfaces.Player;
using SFC.Player.Messages.Events.Player.General;

namespace SFC.Player.Infrastructure.Services.Player;
public class PlayerService(IMapper mapper, IPublishEndpoint publisher) : IPlayerService
{
    private readonly IPublishEndpoint _publisher = publisher;
    private readonly IMapper _mapper = mapper;

    public Task NotifyPlayerCreatedAsync(PlayerEntity player, CancellationToken cancellationToken = default)
    {
        PlayerCreated @event = _mapper.Map<PlayerCreated>(player);
        return _publisher.Publish(@event, cancellationToken);
    }

    public Task NotifyPlayerUpdatedAsync(PlayerEntity player, CancellationToken cancellationToken = default)
    {
        PlayerUpdated @event = _mapper.Map<PlayerUpdated>(player);
        return _publisher.Publish(@event, cancellationToken);
    }
}
