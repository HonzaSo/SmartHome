using MediatR;
using Microsoft.Extensions.Logging;
using SmartHome.Application.Enums;
using SmartHome.Application.Interfaces;

namespace SmartHome.Application.Operations.Rooms.Commands.UpdateRoom;

public class UpdateRoomCommandHandler(IRoomRepository roomRepository, ILogger<UpdateRoomCommandHandler> logger) : IRequestHandler<UpdateRoomCommand, UpdateResultStatus>
{
    public async Task<UpdateResultStatus> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var room = await roomRepository.GetRoomByIdAsync(request.Id, cancellationToken);

            if (room == null)
            {
                logger.LogWarning("Room with ID {RoomId} was not found for update.", request.Id);
                return UpdateResultStatus.NotFound;
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
            logger.LogInformation("Room with ID {RoomId} was successfully updated.", request.Id);
            
            return UpdateResultStatus.Success;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating room with ID {RoomId}", request.Id);
            return UpdateResultStatus.Error;
        }
    }
}

