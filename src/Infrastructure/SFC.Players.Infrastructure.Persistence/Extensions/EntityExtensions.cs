using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Domain.Common;

namespace SFC.Players.Infrastructure.Persistence.Extensions;
public static class EntityExtensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
    {
        return entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }

    public static void SetAuditable(this IEnumerable<EntityEntry<BaseAuditableEntity>> entries,
        IUserService userService, IDateTimeService dateTimeService)
    {
        foreach (EntityEntry<BaseAuditableEntity> entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = userService.UserId;
                entry.Entity.CreatedDate = dateTimeService.Now;
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.LastModifiedBy = userService.UserId;
                entry.Entity.LastModifiedDate = dateTimeService.Now;
            }
        }
    }

    public static void SetAuditable(this IEnumerable<EntityEntry<BaseDataEntity>> entries,
        IDateTimeService dateTimeService)
    {
        foreach (EntityEntry<BaseDataEntity> entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = dateTimeService.Now;
            }
        }
    }
}
