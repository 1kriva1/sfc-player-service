//using MediatR;

//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;

//using Moq;

//using SFC.Player.Application.Interfaces.Common;
//using SFC.Player.Domain.Entities;
//using SFC.Player.Domain.Entities.Identity;
//using SFC.Player.Infrastructure.Persistence.Contexts;
//using SFC.Player.Infrastructure.Persistence.Interceptors;

//using SFC.Player.Infrastructure.Persistence.Repositories;
//using SFC.Player.Infrastructure.Persistence.Repositories.Identity;

//using PlayerEntity = SFC.Player.Domain.Entities.Player;

//namespace SFC.Player.Infrastructure.Persistence.UnitTests.Repositories;
//public class UserRepositoryTests
//{
//    private readonly DbContextOptions<IdentityDbContext> dbContextOptions;

//    public UserRepositoryTests()
//    {
//        dbContextOptions = new DbContextOptionsBuilder<IdentityDbContext>()
//            .UseInMemoryDatabase($"UserRepositoryTestsDb_{DateTime.Now.ToFileTimeUtc()}")
//        .Options;
//    }

//    //[Fact]
//    //[Trait("Persistence", "Repository")]
//    //public async Task Persistence_Repository_User_ShouldFindUserEntityByUserId()
//    //{
//    //    // Arrange
//    //    UserRepository repository = CreateRepository();
//    //    Guid userId = Guid.NewGuid();
//    //    User entity = new()
//    //    {
//    //        Id = userId,
//    //        Player = new PlayerEntity { Id = 1 }
//    //    };

//    //    // Act
//    //    await repository.AddAsync(entity);
//    //    bool result = await repository.AnyAsync(userId);

//    //    // Assert
//    //    Assert.True(result);
//    //}

//    //[Fact]
//    //[Trait("Persistence", "Repository")]
//    //public async Task Persistence_Repository_User_ShouldNotFindUserEntityByUserId()
//    //{
//    //    // Arrange
//    //    UserRepository repository = CreateRepository();
//    //    Guid userId = Guid.NewGuid();
//    //    User entity = new()
//    //    {
//    //        Id = userId,
//    //        Player = new PlayerEntity { Id = 1 }
//    //    };

//    //    // Act
//    //    await repository.AddAsync(entity);
//    //    bool result = await repository.AnyAsync(Guid.NewGuid());

//    //    // Assert
//    //    Assert.False(result);
//    //}

//    //[Fact]
//    //[Trait("Persistence", "Repository")]
//    //public async Task Persistence_Repository_User_ShouldFindUserEntityByUserIdAndPlayerId()
//    //{
//    //    // Arrange
//    //    UserRepository repository = CreateRepository();
//    //    Guid userId = Guid.NewGuid();
//    //    User entity = new()
//    //    {
//    //        Id = userId,
//    //        Player = new PlayerEntity { Id = 1 }
//    //    };

//    //    // Act
//    //    await repository.AddAsync(entity);
//    //    bool result = await repository.AnyAsync(1, userId);

//    //    // Assert
//    //    Assert.True(result);
//    //}

//    //[Fact]
//    //[Trait("Persistence", "Repository")]
//    //public async Task Persistence_Repository_User_ShouldNotFindUserEntityByUserIdAndPlayerId()
//    //{
//    //    // Arrange
//    //    UserRepository repository = CreateRepository();
//    //    Guid userId = Guid.NewGuid();
//    //    User entity = new()
//    //    {
//    //        Id = userId,
//    //        Player = new PlayerEntity { Id = 1 }
//    //    };

//    //    // Act
//    //    await repository.AddAsync(entity);
//    //    bool resultByInvalidPlayerId = await repository.AnyAsync(2, userId);
//    //    bool resultByInvalidUserId = await repository.AnyAsync(1, Guid.NewGuid());
//    //    bool resultByInvalidPlayerAndUserId = await repository.AnyAsync(2, Guid.NewGuid());

//    //    // Assert
//    //    Assert.False(resultByInvalidPlayerId);
//    //    Assert.False(resultByInvalidUserId);
//    //    Assert.False(resultByInvalidPlayerAndUserId);
//    //}

//    private UserRepository CreateRepository()
//    {
//        Mock<IMediator> mediatorMock = new();

//        Mock<IHttpContextAccessor> httpContextAccessorMock = new();

//        Mock<IDateTimeService> dateTimeServiceMock = new();

//        Mock<AuditableEntitySaveChangesInterceptor> interceptorMock = new(httpContextAccessorMock.Object, dateTimeServiceMock.Object);

//        IdentityDbContext context = new(dbContextOptions, mediatorMock.Object, interceptorMock.Object);

//        return new UserRepository(context);
//    }
//}
