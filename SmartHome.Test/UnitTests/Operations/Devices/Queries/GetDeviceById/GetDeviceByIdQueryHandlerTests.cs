using FluentAssertions;
using NSubstitute;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Devices.Queries.GetDeviceById;
using SmartHome.Domain.Domains;
using SmartHome.Domain.Enums;

namespace SmartHome.Test.UnitTests.Operations.Devices.Queries.GetDeviceById;

public class GetDeviceByIdQueryHandlerTests
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly GetDeviceByIdQueryHandler _handler;

    public GetDeviceByIdQueryHandlerTests()
    {
        _deviceRepository = Substitute.For<IDeviceRepository>();
        _handler = new GetDeviceByIdQueryHandler(_deviceRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnDevice_WhenDeviceExists()
    {
        var deviceId = Guid.NewGuid();
        var device = new Device
        {
            Id = deviceId,
            Name = "Světlo",
            Model = "Model X",
            Manufacturer = "Manufacturer",
            State = DeviceState.Online
        };

        _deviceRepository.GetByIdAsync(deviceId, Arg.Any<CancellationToken>()).Returns(device);

        var query = new GetDeviceByIdQuery { Id = deviceId };

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(deviceId);
        result.Name.Should().Be("Světlo");
        await _deviceRepository.Received(1).GetByIdAsync(deviceId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenDeviceDoesNotExist()
    {
        var deviceId = Guid.NewGuid();
        _deviceRepository.GetByIdAsync(deviceId, Arg.Any<CancellationToken>()).Returns((Device?)null);

        var query = new GetDeviceByIdQuery { Id = deviceId };

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeNull();
        await _deviceRepository.Received(1).GetByIdAsync(deviceId, Arg.Any<CancellationToken>());
    }
}

