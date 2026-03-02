using MediatR;
using Microsoft.Extensions.Logging;
using SmartHome.Application.Commands;
using SmartHome.Application.Enums;
using SmartHome.Application.Interfaces;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Handlers;

public class UpdateHomeCommandHandler(IHomeRepository homeRepository, ILogger<UpdateHomeCommandHandler> logger) : IRequestHandler<UpdateHomeCommand, UpdateResult>
{
    public async Task<UpdateResult> Handle(UpdateHomeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var home = await homeRepository.GetByIdAsync(request.Id, cancellationToken);

            if (home == null)
            {
                logger.LogWarning("Domov s ID {HomeId} nebyl nalezen pro aktualizaci.", request.Id);
                return UpdateResult.NotFound;
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
            logger.LogInformation("Domov {HomeId} byl úspěšně aktualizován.", request.Id);
            
            return UpdateResult.Success;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Chyba při aktualizaci domu {HomeId}", request.Id);
            return UpdateResult.Error;
        }
    }
}

