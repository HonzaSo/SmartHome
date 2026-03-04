using MediatR;
using Microsoft.Extensions.Logging;
using SmartHome.Application.Interfaces;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Operations.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler(IRoomRepository roomRepository, ILogger<CreateRoomCommandHandler> logger) : IRequestHandler<CreateRoomCommand, Guid>
{
    public async Task<Guid> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = new Room
        {
            Name =  request.Name,
            Type = request.Type
        };
        
        return await roomRepository.AddAsync(request.HomeId, room, cancellationToken);
    }
}