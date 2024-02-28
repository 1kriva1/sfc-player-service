using SFC.Data.Contracts.Models;
using SFC.Data.Contracts.Models.Common;
using SFC.Players.Domain.Entities.Data;
using SFC.Players.Infrastructure.Extensions;

namespace SFC.Players.Infrastructure.UnitTests.Extensions;
public class EventsExtensionsTests
{
    [Fact]
    [Trait("Extension", "Events")]
    public void Extension_Events_ShouldMapBaseDataEntity()
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
    [Trait("Extension", "Events")]
    public void Extension_Events_ShouldMapStatTypeEntity()
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
