using MediatR;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Homes.Queries.GetHomeById;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Operations.Rooms.Queries.GetRoomByHomeId;

public class GetRoomsByHomeIdQueryHandler(IRoomRepository roomRepository, IMediator mediator) : IRequestHandler<GetRoomsByHomeIdQuery, List<Room>?>
{
    public async Task<List<Room>?> Handle(GetRoomsByHomeIdQuery request, CancellationToken cancellationToken)
    {
        var home = await mediator.Send(new GetHomeByIdQuery { Id = request.Id }, cancellationToken);

        if (home == null)
        {
            return null;
        }
        
        return await roomRepository.GetAllRoomsByHomeIdAsync(request.Id, cancellationToken);
    }
}