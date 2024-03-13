using MediatR;

using Microsoft.EntityFrameworkCore;

using SFC.Player.Domain.Common;
using SFC.Player.Domain.Common.Interfaces;

namespace SFC.Player.Infrastructure.Persistence.Extensions;
public static class MediatorExtensions
{
    public static async Task DispatchDomainEvents(this IMediator mediator, DbContext context)
    {
        IEnumerable<IEntity> entities = context.ChangeTracker
            .Entries<IEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        List<BaseEvent> domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (BaseEvent domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
