//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;

//using SFC.Player.Application.Common.Constants;
//using SFC.Player.Domain.Common;
//using SFC.Player.Domain.Entities;
//using SFC.Player.Domain.Entities.Data;
//using SFC.Player.Infrastructure.Persistence.Configurations;
//using SFC.Player.Infrastructure.Persistence.Configurations.Data;

//using Moq;
//using SFC.Player.Infrastructure.Persistence.Constants;
//using SFC.Player.Messages.Models.Player;

//using IdentityUser = SFC.Player.Domain.Entities.Identity.User;
//using IdentityUserConfiguration = SFC.Player.Infrastructure.Persistence.Configurations.Identity.UserConfiguration;
//using PlayerEntity = SFC.Player.Domain.Entities.Player;
//using PlayerAvailability = SFC.Player.Domain.Entities.PlayerAvailability;
//using PlayerAvailableDay = SFC.Player.Domain.Entities.PlayerAvailableDay;
//using PlayerFootballProfile = SFC.Player.Domain.Entities.PlayerFootballProfile;
//using PlayerGeneralProfile = SFC.Player.Domain.Entities.PlayerGeneralProfile;
//using PlayerPhoto = SFC.Player.Domain.Entities.PlayerPhoto;
//using PlayerTag = SFC.Player.Domain.Entities.PlayerTag;
//using PlayerStat = SFC.Player.Domain.Entities.PlayerStat;
//using PlayerStatPoints = SFC.Player.Domain.Entities.PlayerStatPoints;

//namespace SFC.Player.Infrastructure.Persistence.UnitTests.Configurations;
//public class ConfigurationTests
//{
//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_BaseAuditableEntity_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        BaseAuditableEntityConfiguration<BaseAuditableEntity<PlayerEntity>> sut = new();
//        EntityTypeBuilder<BaseAuditableEntity<PlayerEntity>> builder = GetEntityBuilder<BaseAuditableEntity<PlayerEntity>>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

//        Assert.Equal(4, properties.Count());
//        foreach (IMutableProperty property in properties)
//        {
//            Assert.False(property.IsColumnNullable());
//        }
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_BaseEntity_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        BaseEntityConfiguration<BaseEntity<long>, long> sut = new();
//        EntityTypeBuilder<BaseEntity<long>> builder = GetEntityBuilder<BaseEntity<long>>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

//        Assert.Single(properties);

//        IMutableProperty idProperty = properties.First();

//        Assert.True(idProperty.IsKey());
//        Assert.False(idProperty.IsColumnNullable());
//        Assert.Equal(0, idProperty.GetColumnOrder());
//        Assert.Equal(ValueGenerated.OnAdd, idProperty.ValueGenerated);
//        Assert.Equal(nameof(BaseEntity<long>.Id), idProperty.Name);
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_IdentityUser_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        IdentityUserConfiguration sut = new(true);
//        EntityTypeBuilder<IdentityUser> builder = GetEntityBuilder<IdentityUser>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

//        Assert.Single(properties);

//        IMutableProperty userIdProperty = properties.First();

//        Assert.True(userIdProperty.IsKey());
//        Assert.Equal(nameof(IdentityUser.Id), userIdProperty.Name);
//        Assert.Equal(DatabaseConstants.IDENTITY_SCHEMA_NAME, builder.Metadata.GetSchema());
//        Assert.Equal("Users", builder.Metadata.GetTableName());
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_PlayerAvailability_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        PlayerAvailabilityConfiguration sut = new();
//        EntityTypeBuilder<PlayerAvailability> builder = GetEntityBuilder<PlayerAvailability>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

//        Assert.Equal(3, properties.Count());

//        IMutableProperty fromProperty = properties.First(p => p.Name == nameof(PlayerAvailability.From));

//        Assert.True(fromProperty.IsColumnNullable());

//        IMutableProperty toProperty = properties.First(p => p.Name == nameof(PlayerAvailability.To));

//        Assert.True(toProperty.IsColumnNullable());

//        IEnumerable<IMutableForeignKey> foreignKeys = builder.Metadata.GetDeclaredReferencingForeignKeys();

//        Assert.Single(foreignKeys);

//        Assert.Equal("AvailabilityId", foreignKeys.First().Properties[0].Name);

//        IEnumerable<IMutableNavigation> navigations = builder.Metadata.GetNavigations();

//        Assert.Single(navigations);

//        Assert.Equal(nameof(PlayerAvailability.Days), navigations.First().Name);
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_PlayerAvailableDay_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        PlayerAvailableDayConfiguration sut = new();
//        EntityTypeBuilder<PlayerAvailableDay> builder = GetEntityBuilder<PlayerAvailableDay>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

//        Assert.Single(properties);
//        Assert.False(properties.First().IsColumnNullable());
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_Player_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        PlayerConfiguration sut = new();
//        EntityTypeBuilder<PlayerEntity> builder = GetEntityBuilder<PlayerEntity>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

//        Assert.Equal(8, properties.Count());

//        IEnumerable<IMutableNavigation> navigations = builder.Metadata.GetNavigations();

//        IMutableNavigation generalProfileNavigation = navigations.First(n => n.Name == nameof(PlayerEntity.GeneralProfile));
//        Assert.True(generalProfileNavigation.ForeignKey.IsRequired);

//        IMutableNavigation footballProfileNavigation = navigations.First(n => n.Name == nameof(PlayerEntity.FootballProfile));
//        Assert.True(footballProfileNavigation.ForeignKey.IsRequired);

//        IMutableNavigation availabilityNavigation = navigations.First(n => n.Name == nameof(PlayerEntity.Availability));
//        Assert.True(availabilityNavigation.ForeignKey.IsRequired);

//        IMutableNavigation photoNavigation = navigations.First(n => n.Name == nameof(PlayerEntity.Photo));
//        Assert.True(photoNavigation.ForeignKey.IsRequired);

//        IMutableNavigation tagsNavigation = navigations.First(n => n.Name == nameof(PlayerEntity.Tags));
//        Assert.False(tagsNavigation.ForeignKey.IsRequired);
//        Assert.Equal(DatabaseConstants.PLAYER_FOREIGN_KEY, tagsNavigation.ForeignKey.Properties[0].Name);

//        IMutableNavigation pointsNavigation = navigations.First(n => n.Name == nameof(PlayerEntity.Points));
//        Assert.True(pointsNavigation.ForeignKey.IsRequired);

//        IMutableNavigation statsNavigation = navigations.First(n => n.Name == nameof(PlayerEntity.Stats));
//        Assert.False(statsNavigation.ForeignKey.IsRequired);
//        Assert.Equal(DatabaseConstants.PLAYER_FOREIGN_KEY, statsNavigation.ForeignKey.Properties[0].Name);

//        IMutableNavigation userNavigation = navigations.First(n => n.Name == nameof(PlayerEntity.User));
//        Assert.True(userNavigation.ForeignKey.IsRequired);
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_PlayerFootballProfile_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        PlayerFootballProfileConfiguration sut = new();
//        EntityTypeBuilder<PlayerFootballProfile> builder = GetEntityBuilder<PlayerFootballProfile>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

//        Assert.Equal(10, properties.Count());

//        IMutableProperty heightProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.Height));
//        Assert.True(heightProperty.IsColumnNullable());

//        IMutableProperty weightProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.Weight));
//        Assert.True(weightProperty.IsColumnNullable());

//        IMutableProperty positionProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.PositionId));
//        Assert.True(positionProperty.IsColumnNullable());
//        Assert.True(positionProperty.IsForeignKey());

//        IMutableProperty additionalPositionProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.AdditionalPositionId));
//        Assert.True(additionalPositionProperty.IsColumnNullable());
//        Assert.True(positionProperty.IsForeignKey());

//        IMutableProperty workingFootProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.WorkingFootId));
//        Assert.True(workingFootProperty.IsColumnNullable());
//        Assert.True(positionProperty.IsForeignKey());

//        IMutableProperty numberProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.Number));
//        Assert.True(numberProperty.IsColumnNullable());

//        IMutableProperty gameStyleProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.GameStyleId));
//        Assert.True(gameStyleProperty.IsColumnNullable());
//        Assert.True(positionProperty.IsForeignKey());

//        IMutableProperty skillProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.Skill));
//        Assert.True(skillProperty.IsColumnNullable());

//        IMutableProperty weakFootProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.WeakFoot));
//        Assert.True(weakFootProperty.IsColumnNullable());

//        IMutableProperty physicalConditionFootProperty = properties.First(p => p.Name == nameof(PlayerFootballProfile.PhysicalCondition));
//        Assert.True(physicalConditionFootProperty.IsColumnNullable());
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_PlayerGeneralProfile_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        PlayerGeneralProfileConfiguration sut = new();
//        EntityTypeBuilder<PlayerGeneralProfile> builder = GetEntityBuilder<PlayerGeneralProfile>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

//        Assert.Equal(6, properties.Count());

//        IMutableProperty firstNameProperty = properties.First(p => p.Name == nameof(PlayerGeneralProfile.FirstName));
//        Assert.False(firstNameProperty.IsColumnNullable());
//        Assert.Equal(ValidationConstants.NAME_VALUE_MAX_LENGTH, firstNameProperty.GetMaxLength());

//        IMutableProperty lastNameProperty = properties.First(p => p.Name == nameof(PlayerGeneralProfile.LastName));
//        Assert.False(lastNameProperty.IsColumnNullable());
//        Assert.Equal(ValidationConstants.NAME_VALUE_MAX_LENGTH, lastNameProperty.GetMaxLength());

//        IMutableProperty biographyProperty = properties.First(p => p.Name == nameof(PlayerGeneralProfile.Biography));
//        Assert.True(biographyProperty.IsColumnNullable());
//        Assert.Equal(ValidationConstants.DESCRIPTION_VALUE_MAX_LENGTH, biographyProperty.GetMaxLength());

//        IMutableProperty birthdayProperty = properties.First(p => p.Name == nameof(PlayerGeneralProfile.Birthday));
//        Assert.True(birthdayProperty.IsColumnNullable());
//        Assert.Equal("date", birthdayProperty.GetColumnType());

//        IMutableProperty cityProperty = properties.First(p => p.Name == nameof(PlayerGeneralProfile.City));
//        Assert.False(cityProperty.IsColumnNullable());
//        Assert.Equal(ValidationConstants.CITY_VALUE_MAX_LENGTH, cityProperty.GetMaxLength());

//        IMutableProperty freePlayProperty = properties.First(p => p.Name == nameof(PlayerGeneralProfile.FreePlay));
//        Assert.Equal(false, freePlayProperty.GetDefaultValue());
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_PlayerPhoto_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        PlayerPhotoConfiguration sut = new();
//        EntityTypeBuilder<PlayerPhoto> builder = GetEntityBuilder<PlayerPhoto>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

//        Assert.Equal(4, properties.Count());

//        IMutableProperty sourceProperty = properties.First(p => p.Name == nameof(PlayerPhoto.Source));
//        Assert.False(sourceProperty.IsColumnNullable());
//        Assert.Equal("image", sourceProperty.GetColumnType());

//        IMutableProperty extensionProperty = properties.First(p => p.Name == nameof(PlayerPhoto.Extension));
//        Assert.False(extensionProperty.IsColumnNullable());

//        IMutableProperty nameProperty = properties.First(p => p.Name == nameof(PlayerPhoto.Name));
//        Assert.False(nameProperty.IsColumnNullable());

//        IMutableProperty sizeProperty = properties.First(p => p.Name == nameof(PlayerPhoto.Size));
//        Assert.False(sizeProperty.IsColumnNullable());
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_PlayerPoints_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        PlayerPointsConfiguration sut = new();
//        EntityTypeBuilder<PlayerStatPoints> builder = GetEntityBuilder<PlayerStatPoints>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

//        Assert.Equal(2, properties.Count());

//        IMutableProperty availableProperty = properties.First(p => p.Name == nameof(PlayerStatPoints.Available));
//        Assert.Equal((short)0, availableProperty.GetDefaultValue());

//        IMutableProperty usedProperty = properties.First(p => p.Name == nameof(PlayerStatPoints.Used));
//        Assert.Equal((short)0, usedProperty.GetDefaultValue());
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_PlayerStat_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        PlayerStatConfiguration sut = new();
//        EntityTypeBuilder<PlayerStat> builder = GetEntityBuilder<PlayerStat>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        Assert.Equal(nameof(PlayerStat), builder.Metadata.Name);
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_PlayerTag_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        PlayerTagConfiguration sut = new();
//        EntityTypeBuilder<PlayerTag> builder = GetEntityBuilder<PlayerTag>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

//        Assert.Single(properties);

//        IMutableProperty firstNameProperty = properties.First(p => p.Name == nameof(PlayerTag.Value));
//        Assert.False(firstNameProperty.IsColumnNullable());
//        Assert.Equal(ValidationConstants.TAG_VALUE_MAX_LENGTH, firstNameProperty.GetMaxLength());
//    }

//    //[Fact]
//    //[Trait("Persistence", "Configuration")]
//    //public void Persistence_Configuration_User_ShouldHaveCorrectConfiguration()
//    //{
//    //    // Arrange
//    //    PlayerUserConfiguration sut = new();
//    //    EntityTypeBuilder<PlayerUser> builder = GetEntityBuilder<PlayerUser>();

//    //    // Act
//    //    sut.Configure(builder);

//    //    // Assert
//    //    IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

//    //    Assert.Single(properties);

//    //    IMutableProperty userIdProperty = properties.First(p => p.Name == nameof(PlayerUser.Id));
//    //    Assert.True(userIdProperty.IsKey());
//    //}

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_FootballPosition_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        FootballPositionConfiguration sut = new();
//        EntityTypeBuilder<FootballPosition> builder = GetEntityBuilder<FootballPosition>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        AssertBaseDataEntity(builder, 3, "FootballPositions");
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_GameStyle_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        GameStyleConfiguration sut = new();
//        EntityTypeBuilder<GameStyle> builder = GetEntityBuilder<GameStyle>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        AssertBaseDataEntity(builder, 3, "GameStyles");
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_StatCategory_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        StatCategoryConfiguration sut = new();
//        EntityTypeBuilder<StatCategory> builder = GetEntityBuilder<StatCategory>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        AssertBaseDataEntity(builder, 3, "StatCategories");
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_StatSkill_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        StatSkillConfiguration sut = new();
//        EntityTypeBuilder<StatSkill> builder = GetEntityBuilder<StatSkill>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        AssertBaseDataEntity(builder, 3, "StatSkills");
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_StatType_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        StatTypeConfiguration sut = new();
//        EntityTypeBuilder<StatType> builder = GetEntityBuilder<StatType>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        AssertBaseDataEntity(builder, 5, "StatTypes");
//    }

//    [Fact]
//    [Trait("Persistence", "Configuration")]
//    public void Persistence_Configuration_WorkingFoot_ShouldHaveCorrectConfiguration()
//    {
//        // Arrange
//        WorkingFootConfiguration sut = new();
//        EntityTypeBuilder<WorkingFoot> builder = GetEntityBuilder<WorkingFoot>();

//        // Act
//        sut.Configure(builder);

//        // Assert
//        AssertBaseDataEntity(builder, 3, "WorkingFoots");
//    }

//    private static EntityTypeBuilder<T> GetEntityBuilder<T>() where T : class
//    {
//#pragma warning disable EF1001 // Internal EF Core API usage.
//        EntityType entityType = new(typeof(T).Name, typeof(T), new Model(), false, ConfigurationSource.Explicit);

//        EntityTypeBuilder<T> builder = new(entityType);

//        return builder;
//#pragma warning restore EF1001 // Internal EF Core API usage.
//    }

//    private static void AssertBaseDataEntity<T>(EntityTypeBuilder<T> builder, int count = 3, string? tableName = null) where T : BaseDataEntity
//    {
//        IEnumerable<IMutableProperty> properties = builder.Metadata.GetDeclaredProperties();

//        Assert.Equal(count, properties.Count());

//        IMutableProperty idProperty = properties.FirstOrDefault(m => m.Name == nameof(BaseDataEntity.Id))!;
//        Assert.True(idProperty.IsKey());
//        Assert.False(idProperty.IsColumnNullable());
//        Assert.Equal(0, idProperty.GetColumnOrder());
//        Assert.Equal(ValueGenerated.Never, idProperty.ValueGenerated);
//        Assert.Equal(nameof(BaseDataEntity.Id), idProperty.Name);

//        IMutableProperty titleProperty = properties.FirstOrDefault(m => m.Name == nameof(BaseDataEntity.Title))!;
//        Assert.False(titleProperty.IsColumnNullable());

//        IMutableProperty createdDateProperty = properties.FirstOrDefault(m => m.Name == nameof(BaseDataEntity.CreatedDate))!;
//        Assert.False(createdDateProperty.IsColumnNullable());
//        Assert.Equal(1, createdDateProperty.GetColumnOrder());

//        Assert.Equal(DatabaseConstants.DATA_SCHEMA_NAME, builder.Metadata.GetSchema());
//        Assert.Equal(tableName ?? typeof(T).Name, builder.Metadata.GetTableName());
//    }
//}
