using Microsoft.EntityFrameworkCore;

using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Domain.Common;
using SFC.Player.Domain.Common.Interfaces;

namespace SFC.Player.Infrastructure.Persistence.Extensions;
public static class SeedExtensions
{
    public static void SeedEnumValues<TEntity, TEnum>(this ModelBuilder builder, Func<TEnum, TEntity> converter)
       where TEntity : EnumEntity<TEnum>, new()
       where TEnum : struct
    {
        IEnumerable<TEntity> entities = Enum.GetValues(typeof(TEnum)).Cast<object>()
            .Select(value => converter((TEnum)value));
        builder.Entity<TEntity>().HasData(entities);
    }

    public static void SeedDataEnumValues<TEntity, TEnum>(this ModelBuilder builder, Func<TEnum, TEntity> converter)
        where TEntity : EnumDataEntity<TEnum>
        where TEnum : struct
    {
        IEnumerable<TEntity> entities = GetSeedDataEnumValues(converter);
        builder.Entity<TEntity>().HasData(entities);
    }

    public static IEnumerable<TEntity> GetSeedDataEnumValues<TEntity, TEnum>(Func<TEnum, TEntity> converter)
        where TEntity : EnumDataEntity<TEnum>
        where TEnum : struct
    {
        IEnumerable<TEntity> entities = Enum.GetValues(typeof(TEnum)).Cast<object>()
            .Select(value => converter((TEnum)value));

        return entities;
    }

    public static TEntity SetCreatedDate<TEntity>(this TEntity entity, IDateTimeService dateTimeService)
        where TEntity : IDataEntity
    {
        entity.CreatedDate = dateTimeService.Now;
        return entity;
    }
}
