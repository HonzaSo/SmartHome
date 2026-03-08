using FluentAssertions;
using MediatR;
using NSubstitute;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Homes.Queries.GetHomeById;
using SmartHome.Application.Operations.Rooms.Queries.GetRoomByHomeId;
using SmartHome.Domain.Domains;
using SmartHome.Domain.Enums;

namespace SmartHome.Test.UnitTests.Operations.Rooms.Queries.GetRoomsByHomeId;

public class GetRoomsByHomeIdQueryHandlerTests
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMediator _mediator;
    private readonly GetRoomsByHomeIdQueryHandler _handler;

    public GetRoomsByHomeIdQueryHandlerTests()
    {
        _roomRepository = Substitute.For<IRoomRepository>();
        _mediator = Substitute.For<IMediator>();
        _handler = new GetRoomsByHomeIdQueryHandler(_roomRepository, _mediator);
    }

    [Fact]
    public async Task Handle_ShouldReturnRooms_WhenHomeAndRoomsExist()
    {
        var homeId = Guid.NewGuid();
        var home = new Home { Id = homeId, Name = "Test", Address = new Address() };
        var rooms = new List<Room>
        {
            new() { Id = Guid.NewGuid(), Name = "Koupelna", Type = RoomType.Bedroom },
            new() { Id = Guid.NewGuid(), Name = "Kuchyň", Type = RoomType.Kitchen }
        };

        _mediator.Send(Arg.Any<GetHomeByIdQuery>(), Arg.Any<CancellationToken>()).Returns(home);
        _roomRepository.GetAllRoomsByHomeIdAsync(homeId, Arg.Any<CancellationToken>()).Returns(rooms);

        var query = new GetRoomsByHomeIdQuery { Id = homeId };

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().ContainEquivalentOf(rooms[0]);
        result.Should().ContainEquivalentOf(rooms[1]);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenHomeDoesNotExist()
    {
        var homeId = Guid.NewGuid();
        _mediator.Send(Arg.Any<GetHomeByIdQuery>(), Arg.Any<CancellationToken>()).Returns((Home?)null);

        var query = new GetRoomsByHomeIdQuery { Id = homeId };

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeNull();
        await _roomRepository.DidNotReceive().GetAllRoomsByHomeIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoRoomsExist()
    {
        var homeId = Guid.NewGuid();
        var home = new Home { Id = homeId, Name = "Test", Address = new Address() };

        _mediator.Send(Arg.Any<GetHomeByIdQuery>(), Arg.Any<CancellationToken>()).Returns(home);
        _roomRepository.GetAllRoomsByHomeIdAsync(homeId, Arg.Any<CancellationToken>()).Returns(new List<Room>());

        var query = new GetRoomsByHomeIdQuery { Id = homeId };

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}

