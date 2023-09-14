using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Domain.Common;
using SFC.Players.Domain.Entities;
using SFC.Players.Infrastructure.Persistence.Configurations;

namespace SFC.Players.Infrastructure.Persistence.UnitTests.Configurations;
public class ConfigurationTests
{
    [Fact]
    [Trait("Persistence", "Configuration")]
    public void Persistence_Configuration_BaseAuditableEntity_ShouldHaveCorrectConfiguration()
    {
        // Arrange
        BaseAuditableEntityConfiguration<BaseAuditableEntity> sut = new();
        EntityTypeBuilder<BaseAuditableEntity> builder = GetEntityBuilder<BaseAuditableEntity>();

        // Act
        sut.Configure(builder);

        // Assert
        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

        Assert.Equal(4, properties.Count());
        foreach (IMutableProperty property in properties)
        {
            Assert.False(property.IsColumnNullable());
        }
    }

    [Fact]
    [Trait("Persistence", "Configuration")]
    public void Persistence_Configuration_BaseEntity_ShouldHaveCorrectConfiguration()
    {
        // Arrange
        BaseEntityConfiguration<BaseEntity> sut = new();
        EntityTypeBuilder<BaseEntity> builder = GetEntityBuilder<BaseEntity>();

        // Act
        sut.Configure(builder);

        // Assert
        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

        Assert.Single(properties);

        IMutableProperty idProperty = properties.First();

        Assert.True(idProperty.IsKey());
        Assert.False(idProperty.IsColumnNullable());
        Assert.Equal(0, idProperty.GetColumnOrder());
        Assert.Equal(ValueGenerated.OnAdd, idProperty.ValueGenerated);
        Assert.Equal(nameof(BaseEntity.Id), idProperty.Name);
    }

    [Fact]
    [Trait("Persistence", "Configuration")]
    public void Persistence_Configuration_IdentityUser_ShouldHaveCorrectConfiguration()
    {
        // Arrange
        IdentityUserConfiguration sut = new();
        EntityTypeBuilder<IdentityUser> builder = GetEntityBuilder<IdentityUser>();

        // Act
        sut.Configure(builder);

        // Assert
        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

        Assert.Single(properties);

        IMutableProperty userIdProperty = properties.First();

        Assert.True(userIdProperty.IsKey());
        Assert.Equal(nameof(IdentityUser.UserId), userIdProperty.Name);
    }

    [Fact]
    [Trait("Persistence", "Configuration")]
    public void Persistence_Configuration_PlayerAvailability_ShouldHaveCorrectConfiguration()
    {
        // Arrange
        PlayerAvailabilityConfiguration sut = new();
        EntityTypeBuilder<PlayerAvailability> builder = GetEntityBuilder<PlayerAvailability>();

        // Act
        sut.Configure(builder);

        // Assert
        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

        Assert.Equal(3, properties.Count());

        IMutableProperty fromProperty = properties.First(p => p.Name == nameof(PlayerAvailability.From));

        Assert.True(fromProperty.IsColumnNullable());

        IMutableProperty toProperty = properties.First(p => p.Name == nameof(PlayerAvailability.To));

        Assert.True(toProperty.IsColumnNullable());

        IEnumerable<IMutableForeignKey> foreignKeys = builder.Metadata.GetDeclaredReferencingForeignKeys();

        Assert.Single(foreignKeys);

        Assert.Equal("AvailabilityId", foreignKeys.First().Properties.First().Name);

        IEnumerable<IMutableNavigation> navigations = builder.Metadata.GetNavigations();

        Assert.Single(navigations);

        Assert.Equal(nameof(PlayerAvailability.Days), navigations.First().Name);
    }

    [Fact]
    [Trait("Persistence", "Configuration")]
    public void Persistence_Configuration_PlayerAvailableDay_ShouldHaveCorrectConfiguration()
    {
        // Arrange
        PlayerAvailableDayConfiguration sut = new();
        EntityTypeBuilder<PlayerAvailableDay> builder = GetEntityBuilder<PlayerAvailableDay>();

        // Act
        sut.Configure(builder);

        // Assert
        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

        Assert.Single(properties);
        Assert.False(properties.First().IsColumnNullable());
    }

    [Fact]
    [Trait("Persistence", "Configuration")]
    public void Persistence_Configuration_Player_ShouldHaveCorrectConfiguration()
    {
        // Arrange
        PlayerConfiguration sut = new();
        EntityTypeBuilder<Player> builder = GetEntityBuilder<Player>();

        // Act
        sut.Configure(builder);

        // Assert
        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

        Assert.Equal(8, properties.Count());

        IEnumerable<IMutableNavigation> navigations = builder.Metadata.GetNavigations();

        IMutableNavigation generalProfileNavigation = navigations.First(n => n.Name == nameof(Player.GeneralProfile));
        Assert.True(generalProfileNavigation.ForeignKey.IsRequired);

        IMutableNavigation footballProfileNavigation = navigations.First(n => n.Name == nameof(Player.FootballProfile));
        Assert.True(footballProfileNavigation.ForeignKey.IsRequired);

        IMutableNavigation availabilityNavigation = navigations.First(n => n.Name == nameof(Player.Availability));
        Assert.True(availabilityNavigation.ForeignKey.IsRequired);

        IMutableNavigation photoNavigation = navigations.First(n => n.Name == nameof(Player.Photo));
        Assert.True(photoNavigation.ForeignKey.IsRequired);

        IMutableNavigation tagsNavigation = navigations.First(n => n.Name == nameof(Player.Tags));
        Assert.False(tagsNavigation.ForeignKey.IsRequired);
        Assert.Equal(DbConstants.PLAYER_FOREIGN_KEY, tagsNavigation.ForeignKey.Properties.First().Name);

        IMutableNavigation pointsNavigation = navigations.First(n => n.Name == nameof(Player.Points));
        Assert.True(pointsNavigation.ForeignKey.IsRequired);

        IMutableNavigation statsNavigation = navigations.First(n => n.Name == nameof(Player.Stats));
        Assert.False(statsNavigation.ForeignKey.IsRequired);
        Assert.Equal(DbConstants.PLAYER_FOREIGN_KEY, statsNavigation.ForeignKey.Properties.First().Name);

        IMutableNavigation userNavigation = navigations.First(n => n.Name == nameof(Player.User));
        Assert.True(userNavigation.ForeignKey.IsRequired);
    }

    [Fact]
    [Trait("Persistence", "Configuration")]
    public void Persistence_Configuration_PlayerFootballProfile_ShouldHaveCorrectConfiguration()
    {
        // Arrange
        PlayerFootballProfileConfiguration sut = new();
        EntityTypeBuilder<PlayerFootballProfile> builder = GetEntityBuilder<PlayerFootballProfile>();

        // Act
        sut.Configure(builder);

        // Assert
        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

        Assert.Equal(10, properties.Count());

        IMutableProperty heightProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.Height));
        Assert.True(heightProperty.IsColumnNullable());

        IMutableProperty weightProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.Weight));
        Assert.True(weightProperty.IsColumnNullable());

        IMutableProperty positionProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.Position));
        Assert.True(positionProperty.IsColumnNullable());

        IMutableProperty additionalPositionProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.AdditionalPosition));
        Assert.True(additionalPositionProperty.IsColumnNullable());

        IMutableProperty workingFootProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.WorkingFoot));
        Assert.True(workingFootProperty.IsColumnNullable());

        IMutableProperty numberProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.Number));
        Assert.True(numberProperty.IsColumnNullable());

        IMutableProperty gameStyleProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.GameStyle));
        Assert.True(gameStyleProperty.IsColumnNullable());

        IMutableProperty skillProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.Skill));
        Assert.True(skillProperty.IsColumnNullable());

        IMutableProperty weakFootProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.WeakFoot));
        Assert.True(weakFootProperty.IsColumnNullable());

        IMutableProperty physicalConditionFootProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.PhysicalCondition));
        Assert.True(physicalConditionFootProperty.IsColumnNullable());
    }

    [Fact]
    [Trait("Persistence", "Configuration")]
    public void Persistence_Configuration_PlayerGeneralProfile_ShouldHaveCorrectConfiguration()
    {
        // Arrange
        PlayerGeneralProfileConfiguration sut = new();
        EntityTypeBuilder<PlayerGeneralProfile> builder = GetEntityBuilder<PlayerGeneralProfile>();

        // Act
        sut.Configure(builder);

        // Assert
        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

        Assert.Equal(6, properties.Count());

        IMutableProperty firstNameProperty = properties.First(p => p.Name == nameof(PlayerGeneralProfile.FirstName));
        Assert.False(firstNameProperty.IsColumnNullable());
        Assert.Equal(ValidationConstants.NAME_VALUE_MAX_LENGTH, firstNameProperty.GetMaxLength());

        IMutableProperty lastNameProperty = properties.First(p => p.Name == nameof(PlayerGeneralProfile.LastName));
        Assert.False(lastNameProperty.IsColumnNullable());
        Assert.Equal(ValidationConstants.NAME_VALUE_MAX_LENGTH, lastNameProperty.GetMaxLength());

        IMutableProperty biographyProperty = properties.First(p => p.Name == nameof(PlayerGeneralProfile.Biography));
        Assert.True(biographyProperty.IsColumnNullable());
        Assert.Equal(ValidationConstants.DESCRIPTION_VALUE_MAX_LENGTH, biographyProperty.GetMaxLength());

        IMutableProperty birthdayProperty = properties.First(p => p.Name == nameof(PlayerGeneralProfile.Birthday));
        Assert.True(birthdayProperty.IsColumnNullable());
        Assert.Equal("date", birthdayProperty.GetColumnType());

        IMutableProperty cityProperty = properties.First(p => p.Name == nameof(PlayerGeneralProfile.City));
        Assert.False(cityProperty.IsColumnNullable());
        Assert.Equal(ValidationConstants.CITY_VALUE_MAX_LENGTH, cityProperty.GetMaxLength());

        IMutableProperty freePlayProperty = properties.First(p => p.Name == nameof(PlayerGeneralProfile.FreePlay));
        Assert.Equal(false, freePlayProperty.GetDefaultValue());
    }

    [Fact]
    [Trait("Persistence", "Configuration")]
    public void Persistence_Configuration_PlayerPhoto_ShouldHaveCorrectConfiguration()
    {
        // Arrange
        PlayerPhotoConfiguration sut = new();
        EntityTypeBuilder<PlayerPhoto> builder = GetEntityBuilder<PlayerPhoto>();

        // Act
        sut.Configure(builder);

        // Assert
        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

        Assert.Equal(4, properties.Count());

        IMutableProperty sourceProperty = properties.First(p => p.Name == nameof(PlayerPhoto.Source));
        Assert.False(sourceProperty.IsColumnNullable());
        Assert.Equal("image", sourceProperty.GetColumnType());

        IMutableProperty extensionProperty = properties.First(p => p.Name == nameof(PlayerPhoto.Extension));
        Assert.False(extensionProperty.IsColumnNullable());

        IMutableProperty nameProperty = properties.First(p => p.Name == nameof(PlayerPhoto.Name));
        Assert.False(nameProperty.IsColumnNullable());

        IMutableProperty sizeProperty = properties.First(p => p.Name == nameof(PlayerPhoto.Size));
        Assert.False(sizeProperty.IsColumnNullable());
    }

    [Fact]
    [Trait("Persistence", "Configuration")]
    public void Persistence_Configuration_PlayerPoints_ShouldHaveCorrectConfiguration()
    {
        // Arrange
        PlayerPointsConfiguration sut = new();
        EntityTypeBuilder<PlayerStatPoints> builder = GetEntityBuilder<PlayerStatPoints>();

        // Act
        sut.Configure(builder);

        // Assert
        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

        Assert.Equal(2, properties.Count());

        IMutableProperty availableProperty = properties.First(p => p.Name == nameof(PlayerStatPoints.Available));
        Assert.Equal((short)0, availableProperty.GetDefaultValue());

        IMutableProperty usedProperty = properties.First(p => p.Name == nameof(PlayerStatPoints.Used));
        Assert.Equal((short)0, usedProperty.GetDefaultValue());
    }

    [Fact]
    [Trait("Persistence", "Configuration")]
    public void Persistence_Configuration_PlayerStat_ShouldHaveCorrectConfiguration()
    {
        // Arrange
        PlayerStatConfiguration sut = new();
        EntityTypeBuilder<PlayerStat> builder = GetEntityBuilder<PlayerStat>();

        // Act
        sut.Configure(builder);

        // Assert
        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

        Assert.Equal(2, properties.Count());

        IMutableProperty categoryProperty = properties.First(p => p.Name == nameof(PlayerStat.Category));
        Assert.False(categoryProperty.IsColumnNullable());

        IMutableProperty typeProperty = properties.First(p => p.Name == nameof(PlayerStat.Type));
        Assert.False(typeProperty.IsColumnNullable());
    }

    [Fact]
    [Trait("Persistence", "Configuration")]
    public void Persistence_Configuration_PlayerTag_ShouldHaveCorrectConfiguration()
    {
        // Arrange
        PlayerTagConfiguration sut = new();
        EntityTypeBuilder<PlayerTag> builder = GetEntityBuilder<PlayerTag>();

        // Act
        sut.Configure(builder);

        // Assert
        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

        Assert.Single(properties);

        IMutableProperty firstNameProperty = properties.First(p => p.Name == nameof(PlayerTag.Value));
        Assert.False(firstNameProperty.IsColumnNullable());
        Assert.Equal(ValidationConstants.TAG_VALUE_MAX_LENGTH, firstNameProperty.GetMaxLength());
    }

    [Fact]
    [Trait("Persistence", "Configuration")]
    public void Persistence_Configuration_User_ShouldHaveCorrectConfiguration()
    {
        // Arrange
        UserConfiguration sut = new();
        EntityTypeBuilder<User> builder = GetEntityBuilder<User>();

        // Act
        sut.Configure(builder);

        // Assert
        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

        Assert.Single(properties);

        IMutableProperty userIdProperty = properties.First(p => p.Name == nameof(User.UserId));
        Assert.True(userIdProperty.IsKey());

        IEnumerable<IMutableNavigation> navigations = builder.Metadata.GetNavigations();

        IMutableNavigation identityUserNavigation = navigations.First(n => n.Name == nameof(User.IdentityUser));

        Assert.False(identityUserNavigation.ForeignKey.IsRequired);
        Assert.Equal(nameof(User.UserId), identityUserNavigation.ForeignKey.Properties.First().Name);
    }

    private static EntityTypeBuilder<T> GetEntityBuilder<T>() where T : class
    {
#pragma warning disable EF1001 // Internal EF Core API usage.
        EntityType entityType = new(typeof(T).Name, typeof(T), new Model(), false, ConfigurationSource.Explicit);

        EntityTypeBuilder<T> builder = new(entityType);

        return builder;
#pragma warning restore EF1001 // Internal EF Core API usage.
    }
}
