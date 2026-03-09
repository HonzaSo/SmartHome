using MediatR;
using Microsoft.Extensions.Logging;
using SmartHome.Application.Enums;
using SmartHome.Application.Interfaces;

namespace SmartHome.Application.Operations.Homes.Commands.UpdateHome;

public class UpdateHomeCommandHandler(IHomeRepository homeRepository, ILogger<UpdateHomeCommandHandler> logger) : IRequestHandler<UpdateHomeCommand, UpdateResultStatus>
{
    public async Task<UpdateResultStatus> Handle(UpdateHomeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var home = await homeRepository.GetByIdAsync(request.Id, cancellationToken);

            if (home == null)
            {
                logger.LogWarning("Home with ID {HomeId} was not found for update.", request.Id);
                return UpdateResultStatus.NotFound;
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                home.Name = request.Name;
            }

            if (request.Street != null || request.City != null || request.ZipCode != null)
            {
                var address = home.Address;
                
                if (!string.IsNullOrWhiteSpace(request.Street))
                    address.Street = request.Street;
                    
                if (!string.IsNullOrWhiteSpace(request.City))
                    address.City = request.City;
                    
                if (!string.IsNullOrWhiteSpace(request.ZipCode))
                    address.ZipCode = request.ZipCode;

                home.Address = address;
            }

            await homeRepository.UpdateAsync(home, cancellationToken);
            logger.LogInformation("Home with ID {HomeId} was successfully updated.", request.Id);
            
            return UpdateResultStatus.Success;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating home with ID {HomeId}", request.Id);
            return UpdateResultStatus.Error;
        }
    }
}

