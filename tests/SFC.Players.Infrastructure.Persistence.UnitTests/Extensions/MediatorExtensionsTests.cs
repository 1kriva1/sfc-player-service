using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Moq;

using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Domain.Common;
using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Events;
using SFC.Players.Infrastructure.Persistence.Interceptors;

namespace SFC.Players.Infrastructure.Persistence.UnitTests.Extensions;
public class MediatorExtensionsTests
{
    private readonly Mock<IMediator> mediatorMock = new();
    private readonly DbContextOptions<PlayersDbContext> dbContextOptions;

    public MediatorExtensionsTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<PlayersDbContext>()
            .UseInMemoryDatabase($"MediatorExtensionsTestsDb_{DateTime.Now.ToFileTimeUtc()}")
            .Options;
    }

    [Fact]
    [Trait("Persistence", "Extensions")]
    public async Task Persistence_Extensions_Mediator_ShouldClearDomainEventsBeforePublish()
    {
        // Arrange
        Player entity = new()
        {
            GeneralProfile = new PlayerGeneralProfile
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City"
            }
        };
        PlayerCreatedEvent @event = new(entity);
        PlayersDbContext context = CreateDbContext();

        // Act
        await context.Players.AddAsync(entity);
        entity.AddDomainEvent(@event);
        await context.SaveChangesAsync();

        // Assert
        Assert.Empty(entity.DomainEvents);
    }

    [Fact]
    [Trait("Persistence", "Extensions")]
    public async Task Persistence_Extensions_Mediator_ShouldPublishEvent()
    {
        // Arrange
        Player entity = new()
        {
            GeneralProfile = new PlayerGeneralProfile
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City"
            }
        };
        PlayerCreatedEvent @event = new(entity);

        PlayersDbContext context = CreateDbContext();

        // Act
        EntityEntry<Player> addResult = await context.Players.AddAsync(entity);
        entity.AddDomainEvent(@event);
        await context.SaveChangesAsync();

        // Assert
        mediatorMock.Verify(m => m.Publish(It.IsAny<BaseEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Persistence", "Extensions")]
    public async Task Persistence_Extensions_Mediator_ShouldNotPublishEvent()
    {
        // Arrange
        Player entity = new()
        {
            GeneralProfile = new PlayerGeneralProfile
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City"
            }
        };

        PlayersDbContext context = CreateDbContext();

        // Act
        EntityEntry<Player> addResult = await context.Players.AddAsync(entity);
        await context.SaveChangesAsync();

        // Assert
        mediatorMock.Verify(m => m.Publish(It.IsAny<BaseEvent>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    private PlayersDbContext CreateDbContext()
    {
        Mock<IUserService> userServiceMock = new();
        Mock<IDateTimeService> dateTimeServiceMock = new();
        AuditableEntitySaveChangesInterceptor interceptor = new(userServiceMock.Object, dateTimeServiceMock.Object);

        return new(dbContextOptions, mediatorMock.Object, interceptor);
    }
}
