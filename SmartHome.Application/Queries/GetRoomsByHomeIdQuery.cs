using MediatR;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Queries;

public class GetRoomsByHomeIdQuery : IRequest<List<Room>>
{
    public Guid Id { get; set; }
}