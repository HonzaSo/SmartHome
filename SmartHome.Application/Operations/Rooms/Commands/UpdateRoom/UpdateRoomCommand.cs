using MediatR;
using SmartHome.Application.Enums;
using SmartHome.Domain.Enums;

namespace SmartHome.Application.Operations.Rooms.Commands.UpdateRoom;

public record UpdateRoomCommand : IRequest<UpdateResult>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public RoomType? Type { get; init; }
}

