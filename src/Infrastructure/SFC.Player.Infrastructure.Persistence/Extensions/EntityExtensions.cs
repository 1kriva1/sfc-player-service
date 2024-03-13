using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Identity;
using SFC.Player.Domain.Common;
using SFC.Player.Domain.Common.Interfaces;

namespace SFC.Player.Infrastructure.Persistence.Extensions;
public static class EntityExtensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
    {
        return entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }

    public static void SetAuditable(this IEnumerable<EntityEntry<IAuditableEntity>> entries,
        IUserService userService, IDateTimeService dateTimeService)
    {
        foreach (EntityEntry<IAuditableEntity> entry in entries)
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

    public static void SetAuditable(this IEnumerable<EntityEntry<IExternalAuditableEntity>> entries,
        IDateTimeService dateTimeService)
    {
        foreach (EntityEntry<IExternalAuditableEntity> entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = dateTimeService.Now;
            }
        }
    }
}
