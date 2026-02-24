using MediatR;
using Microsoft.Extensions.Logging;
using SmartHome.Application.Commands;
using SmartHome.Application.Enums;
using SmartHome.Application.Interfaces;

namespace SmartHome.Application.Handlers;

public class RemoveDeviceCommandHandler(IDeviceRepository deviceRepository, ILogger<RemoveDeviceCommandHandler> logger) : IRequestHandler<RemoveDeviceCommand, DeleteResultStatus>
{
    public async Task<DeleteResultStatus> Handle(RemoveDeviceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var device = await deviceRepository.GetByIdAsync(request.Id, cancellationToken);

            if (device == null)
            {
                logger.LogWarning("Zařízení s ID {DeviceId} nebyl nalezeno pro smazání.", request.Id);
                return DeleteResultStatus.NotFound;
            }

            await deviceRepository.RemoveByIdAsync(request.Id, cancellationToken);
            return DeleteResultStatus.Deleted;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Chyba při mazání zařízení {DeviceId}", request.Id);
            return DeleteResultStatus.Error;
        }
    }
}

