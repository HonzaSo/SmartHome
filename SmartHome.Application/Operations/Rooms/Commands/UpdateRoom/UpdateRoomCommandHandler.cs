using MediatR;
using Microsoft.Extensions.Logging;
using SmartHome.Application.Enums;
using SmartHome.Application.Interfaces;

namespace SmartHome.Application.Operations.Rooms.Commands.UpdateRoom;

public class UpdateRoomCommandHandler(IRoomRepository roomRepository, ILogger<UpdateRoomCommandHandler> logger) : IRequestHandler<UpdateRoomCommand, UpdateResult>
{
    public async Task<UpdateResult> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var room = await roomRepository.GetRoomByIdAsync(request.Id, cancellationToken);

            if (room == null)
            {
                logger.LogWarning("Místnost s ID {RoomId} nebyla nalezena pro aktualizaci.", request.Id);
                return UpdateResult.NotFound;
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                room.Name = request.Name;
            }

            if (request.Type.HasValue)
            {
                room.Type = request.Type.Value;
            }

            await roomRepository.UpdateAsync(room, cancellationToken);
            logger.LogInformation("Místnost {RoomId} byla úspěšně aktualizována.", request.Id);
            
            return UpdateResult.Success;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Chyba při aktualizaci místnosti {RoomId}", request.Id);
            return UpdateResult.Error;
        }
    }
}

