using MediatR;

using Microsoft.Extensions.Hosting;

using SFC.Player.Application.Interfaces.Identity;
using SFC.Player.Application.Interfaces.Metadata;
using SFC.Player.Domain.Enums.Metadata;
using SFC.Player.Domain.Events.Data;

namespace SFC.Player.Application.Features.Data.Notifications.DataReseted;

public class DataResetedNotificationHandler(
    IHostEnvironment hostEnvironment,
    IUserSeedService userSeedService,
    IMetadataService metadataService)
    : INotificationHandler<DataResetedEvent>
{
    private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
    private readonly IUserSeedService _userSeedService = userSeedService;
    private readonly IMetadataService _metadataService = metadataService;

    public async Task Handle(DataResetedEvent notification, CancellationToken cancellationToken)
    {
        await _metadataService.CompleteAsync(MetadataService.Data, MetadataDomainEnum.Data, MetadataType.Initialization).ConfigureAwait(false);

        if (_hostEnvironment.IsDevelopment())
        {
            // require seed users
            await _userSeedService.SendRequireUsersSeedAsync(cancellationToken)
                              .ConfigureAwait(false);            
        }
    }
}
