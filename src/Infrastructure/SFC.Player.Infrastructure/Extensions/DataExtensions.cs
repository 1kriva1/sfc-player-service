using SFC.Data.Messages.Models;
using SFC.Data.Messages.Models.Common;
using SFC.Player.Domain.Common;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Infrastructure.Extensions;
public static class DataExtensions
{
    public static T MapToDataEntity<T>(this DataValue value) where T : BaseDataEntity, new()
        => new() { Id = value.Id, Title = value.Title };

    public static StatType MapToDataEntity(this StatTypeDataValue value, StatCategory[] categories, StatSkill[] skills)
       => new() { Id = value.Id, Title = value.Title, Category = categories.FirstOrDefault(c => c.Id == value.CategoryId)!, Skill = skills.FirstOrDefault(c => c.Id == value.SkillId)! };
}
