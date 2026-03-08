using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Rooms.Commands.CreateRoom;
using SmartHome.Domain.Domains;
using SmartHome.Domain.Enums;

namespace SmartHome.Test.UnitTests.Operations.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandlerTests
{
    private readonly IRoomRepository _roomRepository;
    private readonly ILogger<CreateRoomCommandHandler> _logger;
    private readonly CreateRoomCommandHandler _handler;

    public CreateRoomCommandHandlerTests()
    {
        _roomRepository = Substitute.For<IRoomRepository>();
        _logger = Substitute.For<ILogger<CreateRoomCommandHandler>>();
        _handler = new CreateRoomCommandHandler(_roomRepository, _logger);
    }

    [Fact]
    public async Task Handle_ShouldReturnGuid_WhenCommandIsValid()
    {
        var homeId = Guid.NewGuid();
        var roomId = Guid.NewGuid();
        _roomRepository.AddAsync(homeId, Arg.Any<Room>(), Arg.Any<CancellationToken>())
            .Returns(roomId);

        var command = new CreateRoomCommand
        {
            HomeId = homeId,
            Name = "Obývací pokoj",
            Type = RoomType.LivingRoom
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(roomId);
        await _roomRepository.Received(1).AddAsync(homeId, Arg.Is<Room>(r =>
            r.Name == "Obývací pokoj" &&
            r.Type == RoomType.LivingRoom), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldCreateRoomWithCorrectData()
    {
        var homeId = Guid.NewGuid();
        var roomId = Guid.NewGuid();
        Room? capturedRoom = null;

        _roomRepository.AddAsync(homeId, Arg.Do<Room>(r => capturedRoom = r), Arg.Any<CancellationToken>())
            .Returns(roomId);

        var command = new CreateRoomCommand
        {
            HomeId = homeId,
            Name = "Kuchyň",
            Type = RoomType.Kitchen
        };

        await _handler.Handle(command, CancellationToken.None);

        capturedRoom.Should().NotBeNull();
        capturedRoom!.Name.Should().Be("Kuchyň");
        capturedRoom.Type.Should().Be(RoomType.Kitchen);
    }

    [Fact]
    public async Task Handle_ShouldCallRepositoryWithCorrectHomeId()
    {
        var homeId = Guid.NewGuid();
        var roomId = Guid.NewGuid();
        _roomRepository.AddAsync(homeId, Arg.Any<Room>(), Arg.Any<CancellationToken>())
            .Returns(roomId);

        var command = new CreateRoomCommand
        {
            HomeId = homeId,
            Name = "Koupelna",
            Type = RoomType.Bathroom
        };

        await _handler.Handle(command, CancellationToken.None);

        await _roomRepository.Received(1).AddAsync(homeId, Arg.Any<Room>(), Arg.Any<CancellationToken>());
    }
}

