using MediatR;
using Microsoft.Extensions.Logging;
using SmartHome.Application.Enums;
using SmartHome.Application.Interfaces;

namespace SmartHome.Application.Operations.Devices.Commands.UpdateDevice;

public class UpdateDeviceCommandHandler(IDeviceRepository deviceRepository, ILogger<UpdateDeviceCommandHandler> logger) : IRequestHandler<UpdateDeviceCommand, UpdateResult>
{
    public async Task<UpdateResult> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var device = await deviceRepository.GetByIdAsync(request.Id, cancellationToken);

            if (device == null)
            {
                logger.LogWarning("Device with ID {DeviceId} was not found for update.", request.Id);
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
            logger.LogInformation("Device with ID {DeviceId} was successfully updated.", request.Id);
            
            return UpdateResult.Success;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating device with ID {DeviceId}", request.Id);
            return UpdateResult.Error;
        }
    }
}

