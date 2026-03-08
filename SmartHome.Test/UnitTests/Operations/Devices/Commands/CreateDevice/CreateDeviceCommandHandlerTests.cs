using FluentAssertions;
using NSubstitute;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Devices.Commands.CreateDevice;
using SmartHome.Domain.Domains;
using SmartHome.Domain.Enums;

namespace SmartHome.Test.UnitTests.Operations.Devices.Commands.CreateDevice;

public class CreateDeviceCommandHandlerTests
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly CreateDeviceCommandHandler _handler;

    public CreateDeviceCommandHandlerTests()
    {
        _deviceRepository = Substitute.For<IDeviceRepository>();
        _handler = new CreateDeviceCommandHandler(_deviceRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnGuid_WhenCommandIsValid()
    {
        var roomId = Guid.NewGuid();
        var deviceId = Guid.NewGuid();
        _deviceRepository.AddAsync(roomId, Arg.Any<Device>(), Arg.Any<CancellationToken>())
            .Returns(deviceId);

        var command = new CreateDeviceCommand
        {
            RoomId = roomId,
            Name = "Světlo",
            Model = "Model X",
            Manufacturer = "Manufacturer",
            State = DeviceState.Online
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(deviceId);
        await _deviceRepository.Received(1).AddAsync(roomId, Arg.Is<Device>(d =>
            d.Name == "Světlo" &&
            d.Model == "Model X" &&
            d.Manufacturer == "Manufacturer" &&
            d.State == DeviceState.Online), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldCreateDeviceWithCorrectData()
    {
        var roomId = Guid.NewGuid();
        var deviceId = Guid.NewGuid();
        Device? capturedDevice = null;

        _deviceRepository.AddAsync(roomId, Arg.Do<Device>(d => capturedDevice = d), Arg.Any<CancellationToken>())
            .Returns(deviceId);

        var command = new CreateDeviceCommand
        {
            RoomId = roomId,
            Name = "Světlo",
            Model = "Model X",
            Manufacturer = "Manufacturer",
            State = DeviceState.Offline
        };

        await _handler.Handle(command, CancellationToken.None);

        capturedDevice.Should().NotBeNull();
        capturedDevice!.Name.Should().Be("Světlo");
        capturedDevice.Model.Should().Be("Model X");
        capturedDevice.Manufacturer.Should().Be("Manufacturer");
        capturedDevice.State.Should().Be(DeviceState.Offline);
        capturedDevice.Id.Should().NotBe(Guid.Empty);
    }
}

