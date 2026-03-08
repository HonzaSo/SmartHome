using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SmartHome.Application.Enums;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Rooms.Commands.UpdateRoom;
using SmartHome.Domain.Domains;
using SmartHome.Domain.Enums;

namespace SmartHome.Test.UnitTests.Operations.Rooms.Commands.UpdateRoom;

public class UpdateRoomCommandHandlerTests
{
    private readonly IRoomRepository _roomRepository;
    private readonly ILogger<UpdateRoomCommandHandler> _logger;
    private readonly UpdateRoomCommandHandler _handler;

    public UpdateRoomCommandHandlerTests()
    {
        _roomRepository = Substitute.For<IRoomRepository>();
        _logger = Substitute.For<ILogger<UpdateRoomCommandHandler>>();
        _handler = new UpdateRoomCommandHandler(_roomRepository, _logger);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenRoomDoesNotExist()
    {
        var roomId = Guid.NewGuid();
        _roomRepository.GetRoomByIdAsync(roomId, Arg.Any<CancellationToken>())
            .Returns((Room?)null);

        var command = new UpdateRoomCommand { Id = roomId, Name = "New Name" };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResult.NotFound);
        await _roomRepository.DidNotReceive().UpdateAsync(Arg.Any<Room>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenRoomNameIsUpdated()
    {
        var roomId = Guid.NewGuid();
        var room = new Room { Id = roomId, Name = "Old Name", Type = RoomType.Bedroom };

        _roomRepository.GetRoomByIdAsync(roomId, Arg.Any<CancellationToken>()).Returns(room);

        var command = new UpdateRoomCommand { Id = roomId, Name = "New Name" };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResult.Success);
        room.Name.Should().Be("New Name");
        await _roomRepository.Received(1).UpdateAsync(room, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenRoomTypeIsUpdated()
    {
        var roomId = Guid.NewGuid();
        var room = new Room { Id = roomId, Name = "Room", Type = RoomType.Bedroom };

        _roomRepository.GetRoomByIdAsync(roomId, Arg.Any<CancellationToken>()).Returns(room);

        var command = new UpdateRoomCommand { Id = roomId, Type = RoomType.Kitchen };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResult.Success);
        room.Type.Should().Be(RoomType.Kitchen);
        await _roomRepository.Received(1).UpdateAsync(room, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenBothFieldsAreUpdated()
    {
        var roomId = Guid.NewGuid();
        var room = new Room { Id = roomId, Name = "Old Name", Type = RoomType.Bedroom };

        _roomRepository.GetRoomByIdAsync(roomId, Arg.Any<CancellationToken>()).Returns(room);

        var command = new UpdateRoomCommand { Id = roomId, Name = "New Name", Type = RoomType.Bathroom };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResult.Success);
        room.Name.Should().Be("New Name");
        room.Type.Should().Be(RoomType.Bathroom);
        await _roomRepository.Received(1).UpdateAsync(room, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenExceptionOccurs()
    {
        var roomId = Guid.NewGuid();
        _roomRepository.GetRoomByIdAsync(roomId, Arg.Any<CancellationToken>())
            .Throws(new Exception("Database error"));

        var command = new UpdateRoomCommand { Id = roomId, Name = "New Name" };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResult.Error);
    }
}

