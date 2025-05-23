using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Identity;
using SFC.Player.Domain.Common.Interfaces;
using SFC.Player.Infrastructure.Persistence.Extensions;

namespace SFC.Player.Infrastructure.Persistence.Interceptors;
public class AuditableEntitySaveChangesInterceptor(
    IDateTimeService dateTimeService, IUserService userService) : SaveChangesInterceptor
{
    private readonly IDateTimeService _dateTimeService = dateTimeService;
    private readonly IUserService _userService = userService;

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context, cancellationToken);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context, CancellationToken cancellationToken = default)
    {
        if (context == null) return;

        SetAuditableEntities(context);
    }

    private void SetAuditableEntities(DbContext context)
    {
        IEnumerable<EntityEntry<IAuditableEntity>> auditableEntries = context.ChangeTracker.Entries<IAuditableEntity>();

        Guid? userId = _userService.GetUserId();

        foreach (EntityEntry<IAuditableEntity> entry in auditableEntries)
        {
            if (entry.State == EntityState.Added)
            {
                if (userId.HasValue)
                    entry.Entity.CreatedBy = userId.Value;

                entry.Entity.CreatedDate = _dateTimeService.Now;
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                if (userId.HasValue)
                    entry.Entity.LastModifiedBy = userId.Value;

                entry.Entity.LastModifiedDate = _dateTimeService.Now;
            }
        }
    }
}