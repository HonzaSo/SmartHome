using MediatR;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Queries;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Handlers;

public class GetRoomsByHomeIdQueryHandler(IRoomRepository roomRepository) : IRequestHandler<GetRoomsByHomeIdQuery, List<Room>>
{
    public async Task<List<Room>> Handle(GetRoomsByHomeIdQuery request, CancellationToken cancellationToken)
    {
        return await roomRepository.GetAllRoomsByHomeIdAsync(request.Id, cancellationToken);
    }
}