using MediatR;
using SmartHome.Domain.Enums;

namespace SmartHome.Application.Commands;

public record CreateRoomCommand : IRequest<Guid>
{
    public Guid HomeId { get; set; }
    public string Name { get; set; }
    public RoomType Type { get; set; }
}