using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Domain.Common;
using SFC.Players.Infrastructure.Persistence.Extensions;

namespace SFC.Players.Infrastructure.Persistence.Interceptors;
public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IUserService _userService;
    private readonly IDateTimeService _dateTimeService;

    public AuditableEntitySaveChangesInterceptor(IUserService userService, IDateTimeService dateTimeService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, 
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (EntityEntry<BaseAuditableEntity> entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _userService.UserId;
                entry.Entity.CreatedDate = _dateTimeService.Now;
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.LastModifiedBy = _userService.UserId;
                entry.Entity.LastModifiedDate = _dateTimeService.Now;
            }
        }
    }
}