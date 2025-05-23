//using MediatR;

//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;

//using Moq;

//using SFC.Player.Application.Common.Enums;
//using SFC.Player.Application.Features.Common.Models;
//using SFC.Player.Application.Features.Common.Models.Filters;
//using SFC.Player.Application.Features.Common.Models.Paging;
//using SFC.Player.Application.Features.Common.Models.Sorting;
//using SFC.Player.Application.Interfaces.Common;
//using SFC.Player.Application.Interfaces.Identity;
//using SFC.Player.Domain.Entities.Data;
//using SFC.Player.Infrastructure.Persistence.Contexts;
//using SFC.Player.Infrastructure.Persistence.Extensions;
//using SFC.Player.Infrastructure.Persistence.Interceptors;

//namespace SFC.Player.Infrastructure.Persistence.UnitTests.Extensions;
//public class PaginationExtensionsTests
//{
//    private readonly DbContextOptions<DataDbContext> dbContextOptions;

//    public PaginationExtensionsTests()
//    {
//        dbContextOptions = new DbContextOptionsBuilder<DataDbContext>()
//            .UseInMemoryDatabase($"ContextExtensionsTestsDb_{DateTime.Now.ToFileTimeUtc()}")
//            .Options;
//    }

//    [Fact]
//    [Trait("Persistence", "Extensions")]
//    public async Task Persistence_Extensions_Pagination_ShouldPaginate()
//    {
//        // Arrange
//        IQueryable<FootballPosition> query = await GetFootballPositionQueryable();
//        Pagination pagination = new() { Page = 1, Size = 2 };

//        // Act
//        PagedList<FootballPosition> result = await query.PaginateAsync(pagination);

//        // Assert
//        Assert.Equal(2, result.Count);
//        Assert.Equal(3, result[0].Id);
//        Assert.Equal(0, result[1].Id);
//    }

//    [Fact]
//    [Trait("Persistence", "Extensions")]
//    public async Task Persistence_Extensions_Pagination_ShouldPaginateWithSorting()
//    {
//        // Arrange
//        IQueryable<FootballPosition> query = await GetFootballPositionQueryable();
//        Pagination pagination = new() { Page = 1, Size = 3 };
//        Sorting<FootballPosition, dynamic>[] sorting = new Sorting<FootballPosition, dynamic>[1] {
//            new() {
//                Condition = true,
//                Direction = SortingDirection.Ascending,
//                Expression = position => position.Id }
//        };
//        Sortings<FootballPosition> sorts = new(sorting);

//        // Act
//        PagedList<FootballPosition> result = await query.PaginateAsync(pagination, sorts);

//        // Assert
//        Assert.Equal(3, result.Count);
//        Assert.Equal(0, result[0].Id);
//        Assert.Equal(1, result[1].Id);
//        Assert.Equal(2, result[2].Id);
//    }

//    [Fact]
//    [Trait("Persistence", "Extensions")]
//    public async Task Persistence_Extensions_Pagination_ShouldPaginateWithSortingAndFilters()
//    {
//        // Arrange
//        IQueryable<FootballPosition> query = await GetFootballPositionQueryable();
//        Pagination pagination = new() { Page = 1, Size = 3 };
//        Sorting<FootballPosition, dynamic>[] sorting = new Sorting<FootballPosition, dynamic>[1] {
//            new() {
//                Condition = true,
//                Direction = SortingDirection.Ascending,
//                Expression = position => position.Id }
//        };
//        Sortings<FootballPosition> sorts = new(sorting);
//        Filter<FootballPosition>[] filter = new Filter<FootballPosition>[1] {
//            new() {
//                Condition = true,
//                Expression = position=>position.Id == 3 || position.Id == 2,
//            }
//        };
//        Filters<FootballPosition> filters = new(filter);

//        // Act
//        PagedList<FootballPosition> result = await query.PaginateAsync(pagination, sorts, filters);

//        // Assert
//        Assert.Equal(2, result.Count);
//        Assert.Equal(2, result[0].Id);
//        Assert.Equal(3, result[1].Id);
//    }

//    [Fact]
//    [Trait("Persistence", "Extensions")]
//    public async Task Persistence_Extensions_Pagination_ShouldPaginateWithParameters()
//    {
//        // Arrange
//        IQueryable<FootballPosition> query = await GetFootballPositionQueryable();        
//        Pagination pagination = new() { Page = 1, Size = 3 };
//        Sorting<FootballPosition, dynamic>[] sorting = new Sorting<FootballPosition, dynamic>[1] {
//            new() {
//                Condition = true,
//                Direction = SortingDirection.Ascending,
//                Expression = position => position.Id }
//        };
//        Sortings<FootballPosition> sorts = new(sorting);
//        Filter<FootballPosition>[] filter = new Filter<FootballPosition>[1] {
//            new() {
//                Condition = true,
//                Expression = position=>position.Id == 3 || position.Id == 2,
//            }
//        };
//        Filters<FootballPosition> filters = new(filter);
//        FindParameters<FootballPosition> parameters = new() {
//            Filters = filters,
//            Pagination = pagination,
//            Sorting = sorts
//        };

//        // Act
//        PagedList<FootballPosition> result = await query.PaginateAsync(parameters);

//        // Assert
//        Assert.Equal(2, result.Count);
//        Assert.Equal(2, result[0].Id);
//        Assert.Equal(3, result[1].Id);
//    }

//    private async Task<IQueryable<FootballPosition>> GetFootballPositionQueryable()
//    {
//        DbContext context = CreateDbContext();
//        DataDbContext playerDbContext = (DataDbContext)context;
//        await playerDbContext.Set<FootballPosition>().AddRangeAsync(
//            new FootballPosition { Id = 3, Title = "Forward" },
//            new FootballPosition { Id = 0, Title = "Goalkeeper" },
//            new FootballPosition { Id = 1, Title = "Defender" },
//            new FootballPosition { Id = 2, Title = "Midfielder" }
//        );
//        await context.SaveChangesAsync();

//        return playerDbContext.FootballPositions.AsQueryable();
//    }

//    private DataDbContext CreateDbContext()
//    {
//        Mock<IUserService> userServiceMock = new();
//        Mock<IDateTimeService> dateTimeServiceMock = new();
//        Mock<IMediator> mediatorMock = new();
//        AuditableEntitySaveChangesInterceptor interceptor = new(userServiceMock.Object, dateTimeServiceMock.Object);

//        return new(dbContextOptions, mediatorMock.Object, interceptor);
//    }
//}
