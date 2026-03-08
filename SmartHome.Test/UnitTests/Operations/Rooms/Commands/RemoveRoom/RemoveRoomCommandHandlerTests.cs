using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SmartHome.Application.Enums;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Rooms.Commands.RemoveRoom;
using SmartHome.Domain.Domains;
using SmartHome.Domain.Enums;

namespace SmartHome.Test.UnitTests.Operations.Rooms.Commands.RemoveRoom;

public class RemoveRoomCommandHandlerTests
{
    private readonly IRoomRepository _roomRepository;
    private readonly ILogger<RemoveRoomCommandHandler> _logger;
    private readonly RemoveRoomCommandHandler _handler;

    public RemoveRoomCommandHandlerTests()
    {
        _roomRepository = Substitute.For<IRoomRepository>();
        _logger = Substitute.For<ILogger<RemoveRoomCommandHandler>>();
        _handler = new RemoveRoomCommandHandler(_roomRepository, _logger);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenRoomDoesNotExist()
    {
        var roomId = Guid.NewGuid();
        _roomRepository.GetRoomByIdAsync(roomId, Arg.Any<CancellationToken>())
            .Returns((Room?)null);

        var command = new RemoveRoomCommand { Id = roomId };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(DeleteResultStatus.NotFound);
        await _roomRepository.DidNotReceive().RemoveByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnDeleted_WhenEverythingIsOk()
    {
        var roomId = Guid.NewGuid();
        var room = new Room { Id = roomId, Name = "Koupelna", Type = RoomType.Bedroom };

        _roomRepository.GetRoomByIdAsync(roomId, Arg.Any<CancellationToken>()).Returns(room);
        _roomRepository.HasDevicesAsync(roomId, Arg.Any<CancellationToken>()).Returns(false);

        var command = new RemoveRoomCommand { Id = roomId };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(DeleteResultStatus.Deleted);
        await _roomRepository.Received(1).RemoveByIdAsync(roomId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnHasRelatedRecords_WhenRoomHasDevices()
    {
        var roomId = Guid.NewGuid();
        var room = new Room { Id = roomId, Name = "Koupelna", Type = RoomType.Bedroom };

        _roomRepository.GetRoomByIdAsync(roomId, Arg.Any<CancellationToken>()).Returns(room);
        _roomRepository.HasDevicesAsync(roomId, Arg.Any<CancellationToken>()).Returns(true);

        var command = new RemoveRoomCommand { Id = roomId };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(DeleteResultStatus.HasRelatedRecords);
        await _roomRepository.DidNotReceive().RemoveByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenExceptionOccurs()
    {
        var roomId = Guid.NewGuid();
        _roomRepository.GetRoomByIdAsync(roomId, Arg.Any<CancellationToken>())
            .Throws(new Exception("Database error"));

        var command = new RemoveRoomCommand { Id = roomId };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(DeleteResultStatus.Error);
    }
}

