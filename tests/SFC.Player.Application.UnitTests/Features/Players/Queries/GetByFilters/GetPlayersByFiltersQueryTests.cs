using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Moq;

using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Features.Common.Dto.Pagination;
using SFC.Player.Application.Features.Common.Models.Paging;
using SFC.Player.Application.Features.Player.Queries.GetByFilters;
using SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Filters;
using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities;
using SFC.Player.Domain.Events;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.UnitTests.Features.Player.Queries.GetByFilters;
public class GetPlayersByFiltersQueryTests
{
    private readonly Guid _userIdMock = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");
    private readonly IMapper _mapper;
    private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();
    private readonly Mock<IDateTimeService> _dateTimeServiceMock = new();
    private readonly Mock<IUriService> _uriServiceMock = new();
    private readonly Mock<IMediator> _mediatorMock = new();

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
            UserId = _userIdMock,
            Filter = new GetPlayersByFiltersFilterDto(),
            Pagination = new PaginationDto(),
            QueryString = "queryString",
            Route = "route",
            Sorting = new List<SortingDto>()
        };

        _playerRepositoryMock.Setup(r => r.GetPageAsync(It.IsAny<PageParameters<PlayerEntity>>()))
            .ReturnsAsync(new PagedList<PlayerEntity>(new List<PlayerEntity>(), 0, new Pagination { Page = 1, Size = 10 }));

        GetPlayersByFiltersQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _dateTimeServiceMock.Object, _uriServiceMock.Object, _mediatorMock.Object);

        // Act
        GetPlayersByFiltersViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        _playerRepositoryMock.Verify(mock => mock.GetPageAsync(It.IsAny<PageParameters<PlayerEntity>>()), Times.Once());
    }

    [Fact]
    [Trait("Feature", "GetPlayersByFilters")]
    public async Task Feature_GetPlayersByFilters_ShouldFoundAndReturnPlayers()
    {
        // Arrange
        GetPlayersByFiltersQuery query = new()
        {
            UserId = _userIdMock,
            Filter = new GetPlayersByFiltersFilterDto(),
            Pagination = new PaginationDto(),
            QueryString = "queryString",
            Route = "route",
            Sorting = new List<SortingDto>()
        };

        _playerRepositoryMock.Setup(r => r.GetPageAsync(It.IsAny<PageParameters<PlayerEntity>>()))
            .ReturnsAsync(new PagedList<PlayerEntity>(new List<PlayerEntity> { new()}, 1, new Pagination { Page = 1, Size = 10 }));

        GetPlayersByFiltersQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _dateTimeServiceMock.Object, _uriServiceMock.Object, _mediatorMock.Object);

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
            UserId = _userIdMock,
            Filter = new GetPlayersByFiltersFilterDto(),
            Pagination = new PaginationDto(),
            QueryString = "queryString",
            Route = "route",
            Sorting = new List<SortingDto>()
        };

        _playerRepositoryMock.Setup(r => r.GetPageAsync(It.IsAny<PageParameters<PlayerEntity>>()))
            .ReturnsAsync(new PagedList<PlayerEntity>(new List<PlayerEntity>(), 0, new Pagination { Page = 1, Size = 10 }));

        GetPlayersByFiltersQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _dateTimeServiceMock.Object, _uriServiceMock.Object, _mediatorMock.Object);

        // Act
        GetPlayersByFiltersViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Items.Any());
        Assert.NotNull(result.Metadata);
        Assert.NotNull(result.Metadata.Links);
    }

    [Fact]
    [Trait("Feature", "GetPlayersByFilters")]
    public async Task Feature_GetPlayersByFilters_ShouldNotPublishPlayersByFiltersEventWhenPlayersNotFound()
    {
        // Arrange
        GetPlayersByFiltersQuery query = new()
        {
            UserId = _userIdMock,
            Filter = new GetPlayersByFiltersFilterDto(),
            Pagination = new PaginationDto(),
            QueryString = "queryString",
            Route = "route",
            Sorting = new List<SortingDto>()
        };

        _playerRepositoryMock.Setup(r => r.GetPageAsync(It.IsAny<PageParameters<PlayerEntity>>()))
            .ReturnsAsync(new PagedList<PlayerEntity>(new List<PlayerEntity>(), 0, new Pagination { Page = 1, Size = 10 }));

        GetPlayersByFiltersQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _dateTimeServiceMock.Object, _uriServiceMock.Object, _mediatorMock.Object);

        // Act
        GetPlayersByFiltersViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        _mediatorMock.Verify(m => m.Publish(It.IsAny<PlayersByFiltersEvent>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Feature", "GetPlayersByFilters")]
    public async Task Feature_GetPlayersByFilters_ShouldNotPublishPlayersByFiltersEventWhenFiltersNotFitRequirements()
    {
        // Arrange
        GetPlayersByFiltersQuery query = new()
        {
            UserId = _userIdMock,
            Filter = new GetPlayersByFiltersFilterDto(),
            Pagination = new PaginationDto(),
            QueryString = "queryString",
            Route = "route",
            Sorting = new List<SortingDto>()
        };

        _playerRepositoryMock.Setup(r => r.GetPageAsync(It.IsAny<PageParameters<PlayerEntity>>()))
            .ReturnsAsync(new PagedList<PlayerEntity>(new List<PlayerEntity> { new() }, 1, new Pagination { Page = 1, Size = 10 }));

        GetPlayersByFiltersQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _dateTimeServiceMock.Object, _uriServiceMock.Object, _mediatorMock.Object);

        // Act
        GetPlayersByFiltersViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        _mediatorMock.Verify(m => m.Publish(It.IsAny<PlayersByFiltersEvent>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Feature", "GetPlayersByFilters")]
    public async Task Feature_GetPlayersByFilters_ShouldPublishPlayersByFiltersEvent()
    {
        // Arrange
        GetPlayersByFiltersQuery query = new()
        {
            UserId = _userIdMock,
            Filter = new GetPlayersByFiltersFilterDto { 
                Profile = new GetPlayersByFiltersProfileFilterDto { 
                    General = new GetPlayersByFiltersGeneralProfileFilterDto { 
                        Name = "Test"
                    } 
                } 
            },
            Pagination = new PaginationDto(),
            QueryString = "queryString",
            Route = "route",
            Sorting = new List<SortingDto>()
        };

        _playerRepositoryMock.Setup(r => r.GetPageAsync(It.IsAny<PageParameters<PlayerEntity>>()))
            .ReturnsAsync(new PagedList<PlayerEntity>(new List<PlayerEntity> { new() }, 1, new Pagination { Page = 1, Size = 10 }));

        GetPlayersByFiltersQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _dateTimeServiceMock.Object, _uriServiceMock.Object, _mediatorMock.Object);

        // Act
        GetPlayersByFiltersViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        _mediatorMock.Verify(m => m.Publish(It.IsAny<PlayersByFiltersEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
