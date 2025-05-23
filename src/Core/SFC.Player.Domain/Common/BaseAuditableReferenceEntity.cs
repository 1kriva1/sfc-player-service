using SFC.Player.Domain.Common.Interfaces;

namespace SFC.Player.Domain.Common;
public abstract class BaseAuditableReferenceEntity<TId> : BaseEntity<TId>, IAuditableReferenceEntity
{
    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public Guid LastModifiedBy { get; set; }
}
