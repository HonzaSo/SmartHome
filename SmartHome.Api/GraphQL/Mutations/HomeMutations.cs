using MediatR;
using SmartHome.Application.Commands;
using SmartHome.Application.Enums;
using SmartHomeApi.GraphQL.Dtos.Homes;
using SmartHomeApi.GraphQL.Enums;

namespace SmartHomeApi.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class HomeMutations (IMediator mediator, ILogger<HomeMutations> logger)
{
    public async Task<Guid> CreateHome(HomeTypeRequest request)
    {
        logger.LogInformation("Creating home {HomeName}", request.Name);
        
        var command = new CreateHomeCommand()
        {
            Name = request.Name, 
            Street = request.Street, 
            City = request.City, 
            ZipCode = request.ZipCode
        };
        
        return await mediator.Send(command);
    }
    
    public async Task<HomeRemovalResult> RemoveHomeById(Guid homeId)
    {
        logger.LogInformation("Removing home by id: {HomeId}", homeId);
        
        var request = new RemoveHomeCommand()
        {
            Id = homeId
        };
        
        var result = await mediator.Send(request);

        return result switch
        {
            DeleteResultStatus.Deleted => HomeRemovalResult.Success,
            DeleteResultStatus.NotFound => HomeRemovalResult.NotFound,
            DeleteResultStatus.HasRelatedRecords => HomeRemovalResult.HasRelatedRecords,
            _ => HomeRemovalResult.Failure
        };
    }

    public async Task<UpdateHomeResult> UpdateHome(Guid homeId, UpdateHomeRequest request)
    {
        logger.LogInformation("Updating home by id: {HomeId}", homeId);

        var command = new UpdateHomeCommand()
        {
            Id = homeId,
            Name = request.Name,
            Street = request.Street,
            City = request.City,
            ZipCode = request.ZipCode
        };

        var result = await mediator.Send(command);

        return result switch
        {
            UpdateResult.Success => UpdateHomeResult.Success,
            UpdateResult.NotFound => UpdateHomeResult.NotFound,
            UpdateResult.ValidationError => UpdateHomeResult.ValidationError,
            _ => UpdateHomeResult.Failure
        };
    }
}