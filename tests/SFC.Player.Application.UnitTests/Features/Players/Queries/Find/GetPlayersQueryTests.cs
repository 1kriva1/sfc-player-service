using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Moq;

using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Features.Common.Dto.Pagination;
using SFC.Player.Application.Features.Common.Models;
using SFC.Player.Application.Features.Common.Models.Paging;
using SFC.Player.Application.Features.Players.Queries.Find;
using SFC.Player.Application.Features.Players.Queries.Find.Dto.Filters;
using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities;
using SFC.Player.Domain.Events;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.UnitTests.Features.Player.Queries.Find;
public class GetPlayersQueryTests
{
    private readonly Guid _userIdMock = Guid.Parse("db69fc8c-cd50-4c99-96b3-9ddb6c49d08b");
    private readonly IMapper _mapper;
    private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();
    private readonly Mock<IDateTimeService> _dateTimeServiceMock = new();
    private readonly Mock<IUriService> _uriServiceMock = new();
    private readonly Mock<IMediator> _mediatorMock = new();

    public GetPlayersQueryTests()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
                                                    .CreateMapper();
    }

    [Fact]
    [Trait("Feature", "GetPlayers")]
    public async Task Feature_GetPlayers_ShouldCallAllRelevantMethods()
    {
        // Arrange
        GetPlayersQuery query = new()
        {
            UserId = _userIdMock,
            Filter = new GetPlayersFilterDto(),
            Pagination = new PaginationDto(),
            QueryString = "queryString",
            Route = "route",
            Sorting = new List<SortingDto>()
        };

        _playerRepositoryMock.Setup(r => r.FindAsync(It.IsAny<FindParameters<PlayerEntity>>()))
            .ReturnsAsync(new PagedList<PlayerEntity>(new List<PlayerEntity>(), 0, new Pagination { Page = 1, Size = 10 }));

        GetPlayersQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _dateTimeServiceMock.Object, _uriServiceMock.Object, _mediatorMock.Object);

        // Act
        GetPlayersViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        _playerRepositoryMock.Verify(mock => mock.FindAsync(It.IsAny<FindParameters<PlayerEntity>>()), Times.Once());
    }

    [Fact]
    [Trait("Feature", "GetPlayers")]
    public async Task Feature_GetPlayers_ShouldFoundAndReturnPlayers()
    {
        // Arrange
        GetPlayersQuery query = new()
        {
            UserId = _userIdMock,
            Filter = new GetPlayersFilterDto(),
            Pagination = new PaginationDto(),
            QueryString = "queryString",
            Route = "route",
            Sorting = new List<SortingDto>()
        };

        _playerRepositoryMock.Setup(r => r.FindAsync(It.IsAny<FindParameters<PlayerEntity>>()))
            .ReturnsAsync(new PagedList<PlayerEntity>(new List<PlayerEntity> { new()}, 1, new Pagination { Page = 1, Size = 10 }));

        GetPlayersQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _dateTimeServiceMock.Object, _uriServiceMock.Object, _mediatorMock.Object);

        // Act
        GetPlayersViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Items);
        Assert.NotNull(result.Metadata);
        Assert.NotNull(result.Metadata.Links);
        Assert.Equal(RequestId.GetPlayers, query.RequestId);
        Assert.Equal(new EventId(4, "GetPlayers"), query.EventId);
    }

    [Fact]
    [Trait("Feature", "GetPlayers")]
    public async Task Feature_GetPlayers_ShouldNotFoundPlayers()
    {
        // Arrange
        GetPlayersQuery query = new()
        {
            UserId = _userIdMock,
            Filter = new GetPlayersFilterDto(),
            Pagination = new PaginationDto(),
            QueryString = "queryString",
            Route = "route",
            Sorting = new List<SortingDto>()
        };

        _playerRepositoryMock.Setup(r => r.FindAsync(It.IsAny<FindParameters<PlayerEntity>>()))
            .ReturnsAsync(new PagedList<PlayerEntity>(new List<PlayerEntity>(), 0, new Pagination { Page = 1, Size = 10 }));

        GetPlayersQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _dateTimeServiceMock.Object, _uriServiceMock.Object, _mediatorMock.Object);

        // Act
        GetPlayersViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Items.Any());
        Assert.NotNull(result.Metadata);
        Assert.NotNull(result.Metadata.Links);
    }

    [Fact]
    [Trait("Feature", "GetPlayers")]
    public async Task Feature_GetPlayers_ShouldNotPublishGetPlayersEventWhenPlayersNotFound()
    {
        // Arrange
        GetPlayersQuery query = new()
        {
            UserId = _userIdMock,
            Filter = new GetPlayersFilterDto(),
            Pagination = new PaginationDto(),
            QueryString = "queryString",
            Route = "route",
            Sorting = new List<SortingDto>()
        };

        _playerRepositoryMock.Setup(r => r.FindAsync(It.IsAny<FindParameters<PlayerEntity>>()))
            .ReturnsAsync(new PagedList<PlayerEntity>(new List<PlayerEntity>(), 0, new Pagination { Page = 1, Size = 10 }));

        GetPlayersQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _dateTimeServiceMock.Object, _uriServiceMock.Object, _mediatorMock.Object);

        // Act
        GetPlayersViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        _mediatorMock.Verify(m => m.Publish(It.IsAny<GetPlayersEvent>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Feature", "GetPlayers")]
    public async Task Feature_GetPlayers_ShouldNotPublishGetPlayersEventWhenFiltersNotFitRequirements()
    {
        // Arrange
        GetPlayersQuery query = new()
        {
            UserId = _userIdMock,
            Filter = new GetPlayersFilterDto(),
            Pagination = new PaginationDto(),
            QueryString = "queryString",
            Route = "route",
            Sorting = new List<SortingDto>()
        };

        _playerRepositoryMock.Setup(r => r.FindAsync(It.IsAny<FindParameters<PlayerEntity>>()))
            .ReturnsAsync(new PagedList<PlayerEntity>(new List<PlayerEntity> { new() }, 1, new Pagination { Page = 1, Size = 10 }));

        GetPlayersQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _dateTimeServiceMock.Object, _uriServiceMock.Object, _mediatorMock.Object);

        // Act
        GetPlayersViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        _mediatorMock.Verify(m => m.Publish(It.IsAny<GetPlayersEvent>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Feature", "GetPlayers")]
    public async Task Feature_GetPlayers_ShouldPublishGetPlayersEvent()
    {
        // Arrange
        GetPlayersQuery query = new()
        {
            UserId = _userIdMock,
            Filter = new GetPlayersFilterDto { 
                Profile = new GetPlayersProfileFilterDto { 
                    General = new GetPlayersGeneralProfileFilterDto { 
                        Name = "Test"
                    } 
                } 
            },
            Pagination = new PaginationDto(),
            QueryString = "queryString",
            Route = "route",
            Sorting = new List<SortingDto>()
        };

        _playerRepositoryMock.Setup(r => r.FindAsync(It.IsAny<FindParameters<PlayerEntity>>()))
            .ReturnsAsync(new PagedList<PlayerEntity>(new List<PlayerEntity> { new() }, 1, new Pagination { Page = 1, Size = 10 }));

        GetPlayersQueryHandler handler = new(_mapper, _playerRepositoryMock.Object, _dateTimeServiceMock.Object, _uriServiceMock.Object, _mediatorMock.Object);

        // Act
        GetPlayersViewModel? result = await handler.Handle(query, new CancellationToken());

        // Assert
        _mediatorMock.Verify(m => m.Publish(It.IsAny<GetPlayersEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
