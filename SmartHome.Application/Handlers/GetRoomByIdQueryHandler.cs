using MediatR;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Queries;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Handlers;

public class GetRoomByIdQueryHandler (IRoomRepository roomRepository) : IRequestHandler<GetRoomByIdQuery, Room?>
{
    public async Task<Room?> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        return await roomRepository.GetRoomByIdAsync(request.Id, cancellationToken);
    }
}