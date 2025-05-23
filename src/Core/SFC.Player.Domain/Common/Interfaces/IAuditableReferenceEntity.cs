namespace SFC.Player.Domain.Common.Interfaces;
public interface IAuditableReferenceEntity
{
    DateTime CreatedDate { get; set; }

    Guid CreatedBy { get; set; }

    DateTime LastModifiedDate { get; set; }

    Guid LastModifiedBy { get; set; }
}
