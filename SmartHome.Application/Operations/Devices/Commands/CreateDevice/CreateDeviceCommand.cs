using MediatR;
using SmartHome.Domain.Enums;

namespace SmartHome.Application.Operations.Devices.Commands.CreateDevice;

public record CreateDeviceCommand : IRequest<Guid>
{
    public Guid RoomId { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public DeviceState State { get; set; }
}

