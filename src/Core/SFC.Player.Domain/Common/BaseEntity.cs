using System.ComponentModel.DataAnnotations.Schema;

using SFC.Player.Domain.Common.Interfaces;

namespace SFC.Player.Domain.Common;
public abstract class BaseEntity<I>: IEntity
{
    public I Id { get; set; } = default!;

    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public virtual void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public virtual void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public virtual void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
