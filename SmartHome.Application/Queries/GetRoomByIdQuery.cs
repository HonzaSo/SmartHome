using MediatR;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Queries;

public class GetRoomByIdQuery : IRequest<Room?>
{
    public Guid Id { get; set; }
}