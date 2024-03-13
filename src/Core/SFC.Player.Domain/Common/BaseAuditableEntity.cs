using SFC.Player.Domain.Common.Interfaces;

namespace SFC.Player.Domain.Common;

public abstract class BaseAuditableEntity<I> : BaseEntity<I>, IAuditableEntity
{
    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public Guid LastModifiedBy { get; set; }
}