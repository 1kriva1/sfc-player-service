using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Moq;

using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Identity;
using SFC.Player.Domain.Common;
using SFC.Player.Domain.Entities;
using SFC.Player.Domain.Events;
using SFC.Player.Infrastructure.Persistence.Interceptors;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Infrastructure.Persistence.UnitTests.Extensions;
public class MediatorExtensionsTests
{
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly DbContextOptions<PlayerDbContext> _dbContextOptions;

    public MediatorExtensionsTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<PlayerDbContext>()
            .UseInMemoryDatabase($"MediatorExtensionsTestsDb_{DateTime.Now.ToFileTimeUtc()}")
            .Options;
    }

    [Fact]
    [Trait("Persistence", "Extensions")]
    public async Task Persistence_Extensions_Mediator_ShouldClearDomainEventsBeforePublish()
    {
        // Arrange
        PlayerEntity entity = new()
        {
            GeneralProfile = new PlayerGeneralProfile
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City"
            }
        };
        PlayerCreatedEvent @event = new(entity);
        PlayerDbContext context = CreateDbContext();

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
        PlayerEntity entity = new()
        {
            GeneralProfile = new PlayerGeneralProfile
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City"
            }
        };
        PlayerCreatedEvent @event = new(entity);

        PlayerDbContext context = CreateDbContext();

        // Act
        EntityEntry<PlayerEntity> addResult = await context.Players.AddAsync(entity);
        entity.AddDomainEvent(@event);
        await context.SaveChangesAsync();

        // Assert
        _mediatorMock.Verify(m => m.Publish(It.IsAny<BaseEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Persistence", "Extensions")]
    public async Task Persistence_Extensions_Mediator_ShouldNotPublishEvent()
    {
        // Arrange
        PlayerEntity entity = new()
        {
            GeneralProfile = new PlayerGeneralProfile
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City"
            }
        };

        PlayerDbContext context = CreateDbContext();

        // Act
        EntityEntry<PlayerEntity> addResult = await context.Players.AddAsync(entity);
        await context.SaveChangesAsync();

        // Assert
        _mediatorMock.Verify(m => m.Publish(It.IsAny<BaseEvent>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    private PlayerDbContext CreateDbContext()
    {
        Mock<IUserService> userServiceMock = new();
        Mock<IDateTimeService> dateTimeServiceMock = new();
        AuditableEntitySaveChangesInterceptor interceptor = new(userServiceMock.Object, dateTimeServiceMock.Object);

        return new(_dbContextOptions, _mediatorMock.Object, interceptor);
    }
}
