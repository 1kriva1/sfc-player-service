using SFC.Data.Contracts.Models;
using SFC.Data.Contracts.Models.Common;
using SFC.Players.Domain.Common;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Infrastructure.Extensions;
public static class EventsExtensions
{
    public static T MapToDataEntity<T>(this DataValue value) where T : BaseDataEntity, new()
        => new() { Id = value.Id, Title = value.Title };

    public static StatType MapToDataEntity(this StatTypeDataValue value)
       => new() { Id = value.Id, Title = value.Title, CategoryId = value.CategoryId, SkillId = value.SkillId };
}
