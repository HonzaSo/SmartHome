using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SmartHome.Application.Enums;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Devices.Commands.UpdateDevice;
using SmartHome.Domain.Domains;
using SmartHome.Domain.Enums;

namespace SmartHome.Test.UnitTests.Operations.Devices.Commands.UpdateDevice;

public class UpdateDeviceCommandHandlerTests
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly ILogger<UpdateDeviceCommandHandler> _logger;
    private readonly UpdateDeviceCommandHandler _handler;

    public UpdateDeviceCommandHandlerTests()
    {
        _deviceRepository = Substitute.For<IDeviceRepository>();
        _logger = Substitute.For<ILogger<UpdateDeviceCommandHandler>>();
        _handler = new UpdateDeviceCommandHandler(_deviceRepository, _logger);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenDeviceDoesNotExist()
    {
        var deviceId = Guid.NewGuid();
        _deviceRepository.GetByIdAsync(deviceId, Arg.Any<CancellationToken>())
            .Returns((Device?)null);

        var command = new UpdateDeviceCommand { Id = deviceId, Name = "New Name" };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResult.NotFound);
        await _deviceRepository.DidNotReceive().UpdateAsync(Arg.Any<Device>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenDeviceNameIsUpdated()
    {
        var deviceId = Guid.NewGuid();
        var device = new Device
        {
            Id = deviceId,
            Name = "Old Name",
            Model = "Model",
            Manufacturer = "Manufacturer",
            State = DeviceState.Online
        };

        _deviceRepository.GetByIdAsync(deviceId, Arg.Any<CancellationToken>()).Returns(device);

        var command = new UpdateDeviceCommand { Id = deviceId, Name = "New Name" };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResult.Success);
        device.Name.Should().Be("New Name");
        await _deviceRepository.Received(1).UpdateAsync(device, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenDeviceModelIsUpdated()
    {
        var deviceId = Guid.NewGuid();
        var device = new Device
        {
            Id = deviceId,
            Name = "Device",
            Model = "Old Model",
            Manufacturer = "Manufacturer",
            State = DeviceState.Online
        };

        _deviceRepository.GetByIdAsync(deviceId, Arg.Any<CancellationToken>()).Returns(device);

        var command = new UpdateDeviceCommand { Id = deviceId, Model = "New Model" };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResult.Success);
        device.Model.Should().Be("New Model");
        await _deviceRepository.Received(1).UpdateAsync(device, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenDeviceManufacturerIsUpdated()
    {
        var deviceId = Guid.NewGuid();
        var device = new Device
        {
            Id = deviceId,
            Name = "Device",
            Model = "Model",
            Manufacturer = "Old Manufacturer",
            State = DeviceState.Online
        };

        _deviceRepository.GetByIdAsync(deviceId, Arg.Any<CancellationToken>()).Returns(device);

        var command = new UpdateDeviceCommand { Id = deviceId, Manufacturer = "New Manufacturer" };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResult.Success);
        device.Manufacturer.Should().Be("New Manufacturer");
        await _deviceRepository.Received(1).UpdateAsync(device, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenDeviceStateIsUpdated()
    {
        var deviceId = Guid.NewGuid();
        var device = new Device
        {
            Id = deviceId,
            Name = "Device",
            Model = "Model",
            Manufacturer = "Manufacturer",
            State = DeviceState.Online
        };

        _deviceRepository.GetByIdAsync(deviceId, Arg.Any<CancellationToken>()).Returns(device);

        var command = new UpdateDeviceCommand { Id = deviceId, State = DeviceState.Offline };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResult.Success);
        device.State.Should().Be(DeviceState.Offline);
        await _deviceRepository.Received(1).UpdateAsync(device, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenMultipleFieldsAreUpdated()
    {
        var deviceId = Guid.NewGuid();
        var device = new Device
        {
            Id = deviceId,
            Name = "Old Name",
            Model = "Old Model",
            Manufacturer = "Old Manufacturer",
            State = DeviceState.Online
        };

        _deviceRepository.GetByIdAsync(deviceId, Arg.Any<CancellationToken>()).Returns(device);

        var command = new UpdateDeviceCommand
        {
            Id = deviceId,
            Name = "New Name",
            Model = "New Model",
            Manufacturer = "New Manufacturer",
            State = DeviceState.Offline
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResult.Success);
        device.Name.Should().Be("New Name");
        device.Model.Should().Be("New Model");
        device.Manufacturer.Should().Be("New Manufacturer");
        device.State.Should().Be(DeviceState.Offline);
        await _deviceRepository.Received(1).UpdateAsync(device, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenExceptionOccurs()
    {
        var deviceId = Guid.NewGuid();
        _deviceRepository.GetByIdAsync(deviceId, Arg.Any<CancellationToken>())
            .Throws(new Exception("Database error"));

        var command = new UpdateDeviceCommand { Id = deviceId, Name = "New Name" };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResult.Error);
    }
}

