using MediatR;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Operations.Rooms.Queries.GetRoomById;

public class GetRoomByIdQuery : IRequest<Room?>
{
    public Guid Id { get; set; }
}