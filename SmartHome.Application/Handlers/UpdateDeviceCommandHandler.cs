using MediatR;
using Microsoft.Extensions.Logging;
using SmartHome.Application.Commands;
using SmartHome.Application.Enums;
using SmartHome.Application.Interfaces;

namespace SmartHome.Application.Handlers;

public class UpdateDeviceCommandHandler(IDeviceRepository deviceRepository, ILogger<UpdateDeviceCommandHandler> logger) : IRequestHandler<UpdateDeviceCommand, UpdateResult>
{
    public async Task<UpdateResult> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var device = await deviceRepository.GetByIdAsync(request.Id, cancellationToken);

            if (device == null)
            {
                logger.LogWarning("Zařízení s ID {DeviceId} nebyl nalezeno pro aktualizaci.", request.Id);
                return UpdateResult.NotFound;
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                device.Name = request.Name;
            }

            if (!string.IsNullOrWhiteSpace(request.Model))
            {
                device.Model = request.Model;
            }

            if (!string.IsNullOrWhiteSpace(request.Manufacturer))
            {
                device.Manufacturer = request.Manufacturer;
            }

            if (request.State.HasValue)
            {
                device.State = request.State.Value;
            }

            await deviceRepository.UpdateAsync(device, cancellationToken);
            logger.LogInformation("Zařízení {DeviceId} bylo úspěšně aktualizováno.", request.Id);
            
            return UpdateResult.Success;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Chyba při aktualizaci zařízení {DeviceId}", request.Id);
            return UpdateResult.Error;
        }
    }
}

