using MediatR;

using Microsoft.Extensions.Hosting;

using SFC.Player.Application.Interfaces.Metadata;
using SFC.Player.Application.Interfaces.Player;
using SFC.Player.Domain.Events.Identity;

namespace SFC.Player.Application.Features.Identity.Notifications.CreateUsers;
public class UsersCreatedNotificationHandler(
    IHostEnvironment hostEnvironment,
    IMetadataService metadataService,
    IPlayerSeedService playerSeedService) : INotificationHandler<UsersCreatedEvent>
{
    private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
    private readonly IMetadataService _metadataService = metadataService;
    private readonly IPlayerSeedService _playerSeedService = playerSeedService;

    public async Task Handle(UsersCreatedEvent notification, CancellationToken cancellationToken)
    {
        if (_hostEnvironment.IsDevelopment())
        {
            await _metadataService.CompleteAsync(MetadataServiceEnum.Identity, MetadataDomainEnum.User, MetadataTypeEnum.Seed).ConfigureAwait(false);

            if (await _metadataService.IsCompletedAsync(MetadataServiceEnum.Data, MetadataDomainEnum.Data, MetadataTypeEnum.Initialization).ConfigureAwait(true))
            {
                if (!await _metadataService.IsCompletedAsync(MetadataServiceEnum.Player, MetadataDomainEnum.Player, MetadataTypeEnum.Seed).ConfigureAwait(true))
                {
                    // seed players
                    await _playerSeedService.SeedPlayersAsync(cancellationToken).ConfigureAwait(false);
                }
            }
        }
    }
}
