using MediatR;
using Microsoft.Extensions.Logging;
using SmartHome.Application.Commands;
using SmartHome.Application.Interfaces;

namespace SmartHome.Application.Handlers;

public class RemoveHomeCommandHandler(IHomeRepository homeRepository, ILogger<RemoveHomeCommandHandler> logger) : IRequestHandler<RemoveHomeCommand, DeleteResultStatus>
{
    public async Task<DeleteResultStatus> Handle(RemoveHomeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var home = await homeRepository.GetByIdAsync(request.Id, cancellationToken);

            if (home == null)
            {
                logger.LogWarning("Domov s ID {HomeId} nebyl nalezen pro smazání.", request.Id);

                return DeleteResultStatus.NotFound;
            }
            
            await homeRepository.RemoveByIdAsync(request.Id, cancellationToken);
            return DeleteResultStatus.Deleted;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Chyba při mazání");
            return DeleteResultStatus.Error;
        }
    }
}

public enum DeleteResultStatus
{
    Deleted,
    NotFound,
    Error
}