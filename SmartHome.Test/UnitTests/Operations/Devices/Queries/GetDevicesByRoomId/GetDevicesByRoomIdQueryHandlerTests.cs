using FluentAssertions;
using MediatR;
using NSubstitute;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Devices.Queries.GetDeviceByRoomId;
using SmartHome.Application.Operations.Rooms.Queries.GetRoomById;
using SmartHome.Domain.Domains;
using SmartHome.Domain.Enums;

namespace SmartHome.Test.UnitTests.Operations.Devices.Queries.GetDevicesByRoomId;

public class GetDevicesByRoomIdQueryHandlerTests
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IMediator _mediator;
    private readonly GetDevicesByRoomIdQueryHandler _handler;

    public GetDevicesByRoomIdQueryHandlerTests()
    {
        _deviceRepository = Substitute.For<IDeviceRepository>();
        _mediator = Substitute.For<IMediator>();
        _handler = new GetDevicesByRoomIdQueryHandler(_deviceRepository, _mediator);
    }

    [Fact]
    public async Task Handle_ShouldReturnDevices_WhenRoomAndDevicesExist()
    {
        var roomId = Guid.NewGuid();
        var room = new Room { Id = roomId, Name = "Bedroom", Type = RoomType.Bedroom };
        var devices = new List<Device>
        {
            new() { Id = Guid.NewGuid(), Name = "Světlo", Model = "X", Manufacturer = "M", State = DeviceState.Online },
            new() { Id = Guid.NewGuid(), Name = "Termostat", Model = "X", Manufacturer = "M", State = DeviceState.Offline }
        };

        _mediator.Send(Arg.Any<GetRoomByIdQuery>(), Arg.Any<CancellationToken>()).Returns(room);
        _deviceRepository.GetAllByRoomIdAsync(roomId, Arg.Any<CancellationToken>()).Returns(devices);

        var query = new GetDevicesByRoomIdQuery { Id = roomId };

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().ContainEquivalentOf(devices[0]);
        result.Should().ContainEquivalentOf(devices[1]);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenRoomDoesNotExist()
    {
        var roomId = Guid.NewGuid();
        _mediator.Send(Arg.Any<GetRoomByIdQuery>(), Arg.Any<CancellationToken>()).Returns((Room?)null);

        var query = new GetDevicesByRoomIdQuery { Id = roomId };

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeNull();
        await _deviceRepository.DidNotReceive().GetAllByRoomIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoDevicesExist()
    {
        var roomId = Guid.NewGuid();
        var room = new Room { Id = roomId, Name = "Koupelna", Type = RoomType.Bedroom };

        _mediator.Send(Arg.Any<GetRoomByIdQuery>(), Arg.Any<CancellationToken>()).Returns(room);
        _deviceRepository.GetAllByRoomIdAsync(roomId, Arg.Any<CancellationToken>()).Returns(new List<Device>());

        var query = new GetDevicesByRoomIdQuery { Id = roomId };

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}

