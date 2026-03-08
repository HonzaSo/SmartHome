using FluentAssertions;
using NSubstitute;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Rooms.Queries.GetRoomById;
using SmartHome.Domain.Domains;
using SmartHome.Domain.Enums;

namespace SmartHome.Test.UnitTests.Operations.Rooms.Queries.GetRoomById;

public class GetRoomByIdQueryHandlerTests
{
    private readonly IRoomRepository _roomRepository;
    private readonly GetRoomByIdQueryHandler _handler;

    public GetRoomByIdQueryHandlerTests()
    {
        _roomRepository = Substitute.For<IRoomRepository>();
        _handler = new GetRoomByIdQueryHandler(_roomRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnRoom_WhenRoomExists()
    {
        var roomId = Guid.NewGuid();
        var room = new Room { Id = roomId, Name = "Koupelna", Type = RoomType.Bedroom };

        _roomRepository.GetRoomByIdAsync(roomId, Arg.Any<CancellationToken>()).Returns(room);

        var query = new GetRoomByIdQuery { Id = roomId };

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(roomId);
        result.Name.Should().Be("Koupelna");
        await _roomRepository.Received(1).GetRoomByIdAsync(roomId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenRoomDoesNotExist()
    {
        var roomId = Guid.NewGuid();
        _roomRepository.GetRoomByIdAsync(roomId, Arg.Any<CancellationToken>()).Returns((Room?)null);

        var query = new GetRoomByIdQuery { Id = roomId };

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeNull();
        await _roomRepository.Received(1).GetRoomByIdAsync(roomId, Arg.Any<CancellationToken>());
    }
}

