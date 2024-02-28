using AutoMapper;

using Microsoft.Extensions.Logging;

using Moq;

using SFC.Players.Application.Common.Enums;
using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Common.Dto;
using SFC.Players.Application.Features.Common.Dto.Pagination;
using SFC.Players.Application.Features.Common.Models.Paging;
using SFC.Players.Application.Features.Players.Queries.GetByFilters;
using SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Filters;
using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.UnitTests.Features.Players.Queries.GetByFilters;
public class GetPlayersByFiltersQueryTests
{
    private readonly Guid MOCK_USER_ID = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");
    private readonly IMapper _mapper;
    private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();
    private readonly Mock<IDateTimeService> _dateTimeServiceMock = new();
    private readonly Mock<IUriService> _uriServiceMock = new();

    public GetPlayersByFiltersQueryTests()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
                                                    .CreateMapper();
    }

    [Fact]
    [Trait("Feature", "GetPlayersByFilters")]
    public async Task Feature_GetPlayersByFilters_ShouldCallAllRelevantMethods()
    {
        // Arrange
        GetPlayersByFiltersQuery query = new()
        {
            UserId = MOCK_USER_ID,
            Filter = new GetPlayersByFiltersFilterDto(),
            Pagination = new PaginationDto(),
            QueryString = "queryString",
            Route = "route",
            Sorting = new List<SortingDto>()
        };

        _playerRepositoryMock.Setup(r => r.GetPageAsync(It.IsAny<PageParameters<Player>>()))
            .ReturnsAsync(new PagedList<Player>(new List<Player>(), 0, new Pagination { Page = 1, Size = 10 }));

        GetPlayersByFiltersQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _dateTimeServiceMock.Object, _uriServiceMock.Object);

        // Act
        GetPlayersByFiltersViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        _playerRepositoryMock.Verify(mock => mock.GetPageAsync(It.IsAny<PageParameters<Player>>()), Times.Once());
    }

    [Fact]
    [Trait("Feature", "GetPlayersByFilters")]
    public async Task Feature_GetPlayersByFilters_ShouldFoundAndReturnPlayers()
    {
        // Arrange
        GetPlayersByFiltersQuery query = new()
        {
            UserId = MOCK_USER_ID,
            Filter = new GetPlayersByFiltersFilterDto(),
            Pagination = new PaginationDto(),
            QueryString = "queryString",
            Route = "route",
            Sorting = new List<SortingDto>()
        };

        _playerRepositoryMock.Setup(r => r.GetPageAsync(It.IsAny<PageParameters<Player>>()))
            .ReturnsAsync(new PagedList<Player>(new List<Player> { new()}, 1, new Pagination { Page = 1, Size = 10 }));

        GetPlayersByFiltersQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _dateTimeServiceMock.Object, _uriServiceMock.Object);

        // Act
        GetPlayersByFiltersViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Items);
        Assert.NotNull(result.Metadata);
        Assert.NotNull(result.Metadata.Links);
        Assert.Equal(RequestId.GetPlayersByFilters, query.RequestId);
        Assert.Equal(new EventId(4, "GetPlayersByFilters"), query.EventId);
    }

    [Fact]
    [Trait("Feature", "GetPlayersByFilters")]
    public async Task Feature_GetPlayersByFilters_ShouldNotFoundPlayers()
    {
        // Arrange
        GetPlayersByFiltersQuery query = new()
        {
            UserId = MOCK_USER_ID,
            Filter = new GetPlayersByFiltersFilterDto(),
            Pagination = new PaginationDto(),
            QueryString = "queryString",
            Route = "route",
            Sorting = new List<SortingDto>()
        };

        _playerRepositoryMock.Setup(r => r.GetPageAsync(It.IsAny<PageParameters<Player>>()))
            .ReturnsAsync(new PagedList<Player>(new List<Player>(), 0, new Pagination { Page = 1, Size = 10 }));

        GetPlayersByFiltersQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _dateTimeServiceMock.Object, _uriServiceMock.Object);

        // Act
        GetPlayersByFiltersViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Items.Any());
        Assert.NotNull(result.Metadata);
        Assert.NotNull(result.Metadata.Links);
    }
}
