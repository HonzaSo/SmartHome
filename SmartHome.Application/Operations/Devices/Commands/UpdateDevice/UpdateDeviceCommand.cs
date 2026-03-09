using MediatR;
using SmartHome.Application.Enums;
using SmartHome.Domain.Enums;

namespace SmartHome.Application.Operations.Devices.Commands.UpdateDevice;

public record UpdateDeviceCommand : IRequest<UpdateResultStatus>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Model { get; init; }
    public string? Manufacturer { get; init; }
    public DeviceState? State { get; init; }
}

