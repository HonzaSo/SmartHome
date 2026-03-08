using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SmartHome.Application.Enums;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Devices.Commands.RemoveDevice;
using SmartHome.Domain.Domains;
using SmartHome.Domain.Enums;

namespace SmartHome.Test.UnitTests.Operations.Devices.Commands.RemoveDevice;

public class RemoveDeviceCommandHandlerTests
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly ILogger<RemoveDeviceCommandHandler> _logger;
    private readonly RemoveDeviceCommandHandler _handler;

    public RemoveDeviceCommandHandlerTests()
    {
        _deviceRepository = Substitute.For<IDeviceRepository>();
        _logger = Substitute.For<ILogger<RemoveDeviceCommandHandler>>();
        _handler = new RemoveDeviceCommandHandler(_deviceRepository, _logger);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenDeviceDoesNotExist()
    {
        var deviceId = Guid.NewGuid();
        _deviceRepository.GetByIdAsync(deviceId, Arg.Any<CancellationToken>())
            .Returns((Device?)null);

        var command = new RemoveDeviceCommand { Id = deviceId };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(DeleteResultStatus.NotFound);
        await _deviceRepository.DidNotReceive().RemoveByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnDeleted_WhenEverythingIsOk()
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

        var command = new RemoveDeviceCommand { Id = deviceId };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(DeleteResultStatus.Deleted);
        await _deviceRepository.Received(1).RemoveByIdAsync(deviceId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenExceptionOccurs()
    {
        var deviceId = Guid.NewGuid();
        _deviceRepository.GetByIdAsync(deviceId, Arg.Any<CancellationToken>())
            .Throws(new Exception("Database error"));

        var command = new RemoveDeviceCommand { Id = deviceId };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(DeleteResultStatus.Error);
    }
}

