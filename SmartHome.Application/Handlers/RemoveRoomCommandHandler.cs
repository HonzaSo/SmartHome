using MediatR;
using Microsoft.Extensions.Logging;
using SmartHome.Application.Commands;
using SmartHome.Application.Enums;
using SmartHome.Application.Interfaces;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Handlers;

public class RemoveRoomCommandHandler(IRoomRepository roomRepository, ILogger<RemoveRoomCommandHandler> logger) : IRequestHandler<RemoveRoomCommand, DeleteResultStatus>
{
    public async Task<DeleteResultStatus> Handle(RemoveRoomCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var room = await roomRepository.GetRoomByIdAsync(request.Id, cancellationToken);

            var validationResult = await ValidateAsync(request.Id, room, cancellationToken);

            if (validationResult.HasValue)
            {
                return validationResult.Value;
            }

            await roomRepository.RemoveByIdAsync(request.Id, cancellationToken);
            return DeleteResultStatus.Deleted;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Chyba při mazání místnosti {RoomId}",  request.Id);
            return DeleteResultStatus.Error;
        }
    }

    private async Task<DeleteResultStatus?> ValidateAsync(Guid id, Room? room, CancellationToken cancellationToken)
    {
        if (room == null)
        {
            logger.LogWarning("Místnost s ID {RoomId} nebyla nalezena pro smazání.", id);
            return DeleteResultStatus.NotFound;
        }

        var hasDevices = await roomRepository.HasDevicesAsync(id, cancellationToken);

        if (hasDevices)
        {
            return DeleteResultStatus.HasRelatedRecords;
        }

        return null;
    }
}
