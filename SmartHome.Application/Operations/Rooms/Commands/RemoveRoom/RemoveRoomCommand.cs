using MediatR;
using SmartHome.Application.Enums;

namespace SmartHome.Application.Operations.Rooms.Commands.RemoveRoom;

public record RemoveRoomCommand : IRequest<DeleteResultStatus>
{
    public Guid Id { get; init; }
}
