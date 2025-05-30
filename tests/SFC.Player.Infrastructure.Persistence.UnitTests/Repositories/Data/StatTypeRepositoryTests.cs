﻿//using MediatR;

//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;

//using Moq;

//using SFC.Player.Application.Interfaces.Common;
//using SFC.Player.Application.Interfaces.Persistence.Repository;
//using SFC.Player.Domain.Entities.Data;
//using SFC.Player.Infrastructure.Persistence.Contexts;
//using SFC.Player.Infrastructure.Persistence.Interceptors;

//using SFC.Player.Infrastructure.Persistence.Repositories;
//using SFC.Player.Infrastructure.Persistence.Repositories.Data;

//namespace SFC.Player.Infrastructure.Persistence.UnitTests.Repositories.Data;
//public class StatTypeRepositoryTests
//{
//    private readonly DbContextOptions<DataDbContext> _dbContextOptions;

//    public StatTypeRepositoryTests()
//    {
//        _dbContextOptions = new DbContextOptionsBuilder<DataDbContext>()
//            .UseInMemoryDatabase($"UserRepositoryTestsDb_{DateTime.Now.ToFileTimeUtc()}")
//        .Options;
//    }

//    [Fact]
//    [Trait("Persistence", "Repository")]
//    public async Task Persistence_Repository_StatType_ShouldReturnCount()
//    {
//        // Arrange
//        IStatTypeRepository repository = CreateRepository();
//        StatType entity = new()
//        {
//            Id = 2,
//            Title = "Title"
//        };

//        // Act
//        await CreateDataRepository().AddAsync(entity);
//        int result = await repository.CountAsync();

//        // Assert
//        Assert.Equal(1, result);
//    }

//    [Fact]
//    [Trait("Persistence", "Repository")]
//    public async Task Persistence_Repository_StatType_ShouldReturnAllEntities()
//    {
//        // Arrange
//        IStatTypeRepository repository = CreateRepository();
//        StatType entity = new()
//        {
//            Id = 2,
//            Title = "Title"
//        };

//        // Act
//        await CreateDataRepository().AddAsync(entity);
//        IReadOnlyList<StatType> result = await repository.ListAllAsync();

//        // Assert
//        Assert.Single(result);
//    }

//    [Fact]
//    [Trait("Persistence", "Repository")]
//    public async Task Persistence_Repository_StatType_ShouldReturnAllAsNoTracking()
//    {
//        // Arrange
//        DataDbContext context = CreateContext();
//        IStatTypeRepository repository = new StatTypeRepository(context);

//        // Act
//        await CreateDataRepository().AddRangeAsync(
//            new StatType() { Id = 1, Title = "Title1" },
//            new StatType() { Id = 2, Title = "Title2" }
//        );
//        IReadOnlyList<StatType> result = await repository.ListAllAsync();

//        // Assert
//        foreach (StatType statType in result)
//        {
//            Assert.Equal(EntityState.Detached, context.Entry(statType).State);
//        }
//    }

//    private DataDbContext CreateContext()
//    {
//        Mock<IMediator> mediatorMock = new();

//        Mock<IHttpContextAccessor> httpContextAccessorMock = new();

//        Mock<IDateTimeService> dateTimeServiceMock = new();

//        Mock<AuditableEntitySaveChangesInterceptor> interceptorMock = new(httpContextAccessorMock.Object, dateTimeServiceMock.Object);

//        return new(_dbContextOptions, mediatorMock.Object, interceptorMock.Object);
//    }

//    private IStatTypeRepository CreateRepository()
//    {
//        DataDbContext context = CreateContext();

//        return new StatTypeRepository(context);
//    }

//    private DataRepository<StatType> CreateDataRepository()
//    {
//        DataDbContext context = CreateContext();

//        return new DataRepository<StatType>(context);
//    }
//}
