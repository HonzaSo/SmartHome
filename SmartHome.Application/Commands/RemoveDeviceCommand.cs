using MediatR;
using SmartHome.Application.Enums;

namespace SmartHome.Application.Commands;

public record RemoveDeviceCommand : IRequest<DeleteResultStatus>
{
    public Guid Id { get; init; }
}

