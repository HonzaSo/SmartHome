using MediatR;
using Microsoft.Extensions.Logging;
using SmartHome.Application.Commands;
using SmartHome.Application.Enums;
using SmartHome.Application.Interfaces;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Handlers;

public class RemoveHomeCommandHandler(IHomeRepository homeRepository, ILogger<RemoveHomeCommandHandler> logger) : IRequestHandler<RemoveHomeCommand, DeleteResultStatus>
{
    public async Task<DeleteResultStatus> Handle(RemoveHomeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var home = await homeRepository.GetByIdAsync(request.Id, cancellationToken);

            var validationResult =  await ValidateAsync(request.Id, home, cancellationToken);

            if (validationResult.HasValue)
            {
                return validationResult.Value;
            }

            await homeRepository.RemoveByIdAsync(request.Id, cancellationToken);
            return DeleteResultStatus.Deleted;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Chyba při mazání {HomeId}",  request.Id);
            return DeleteResultStatus.Error;
        }
    }

    private async Task<DeleteResultStatus?> ValidateAsync(Guid id, Home? home, CancellationToken cancellationToken)
    {
        if (home == null)
        {
            logger.LogWarning("Domov s ID {HomeId} nebyl nalezen pro smazání.", id);

            return DeleteResultStatus.NotFound;
        }
            
        var hasRooms = await homeRepository.HasRoomsAsync(home.Id, cancellationToken);

        if (hasRooms)
        {
            return DeleteResultStatus.HasRelatedRecords;
        }

        return null;
    }
}