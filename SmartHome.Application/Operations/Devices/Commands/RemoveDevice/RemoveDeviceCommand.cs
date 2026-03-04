using MediatR;
using SmartHome.Application.Enums;

namespace SmartHome.Application.Operations.Devices.Commands.RemoveDevice;

public record RemoveDeviceCommand : IRequest<DeleteResultStatus>
{
    public Guid Id { get; init; }
}

