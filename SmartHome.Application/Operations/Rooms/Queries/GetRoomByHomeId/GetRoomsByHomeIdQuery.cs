using MediatR;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Operations.Rooms.Queries.GetRoomByHomeId;

public class GetRoomsByHomeIdQuery : IRequest<List<Room>?>
{
    public Guid Id { get; set; }
}