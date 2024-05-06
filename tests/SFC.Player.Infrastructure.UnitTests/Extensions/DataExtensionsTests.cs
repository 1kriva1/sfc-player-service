using SFC.Data.Messages.Models;
using SFC.Data.Messages.Models.Common;
using SFC.Player.Domain.Entities.Data;
using SFC.Player.Infrastructure.Extensions;

namespace SFC.Player.Infrastructure.UnitTests.Extensions;
public class DataExtensionsTests
{
    [Fact]
    [Trait("Extension", "Data")]
    public void Extension_Data_ShouldMapBaseDataEntity()
    {
        // Arrange
        DataValue value = new() { Id = 0, Title = "Title" };

        // Act
        FootballPosition entity = value.MapToDataEntity<FootballPosition>();

        // Assert
        Assert.Equal(value.Id, entity.Id);
        Assert.Equal(value.Title, entity.Title);
    }

    [Fact]
    [Trait("Extension", "Data")]
    public void Extension_Data_ShouldMapStatTypeEntity()
    {
        // Arrange
        StatTypeDataValue value = new() { Id = 0, Title = "Title", CategoryId = 1, SkillId = 2 };
        StatCategory[] categories = new StatCategory[1] { new() { Id = 1 } };
        StatSkill[] skills = new StatSkill[1] { new() { Id = 2 } };

        // Act
        StatType entity = value.MapToDataEntity(categories, skills);

        // Assert
        Assert.Equal(value.Id, entity.Id);
        Assert.Equal(value.Title, entity.Title);
        Assert.Equal(value.CategoryId, entity.Category.Id);
        Assert.Equal(value.SkillId, entity.Skill.Id);
    }
}
