using MediatR;
using SmartHome.Application.Commands;
using SmartHomeApi.GraphQL.Types;

namespace SmartHomeApi.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class HomeMutations (IMediator mediator, ILogger<HomeMutations> logger)
{
    public async Task<Guid> CreateHome(HomeTypeInput input)
    {
        logger.LogInformation("Creating home {HomeName}", input.Name);
        
        var request = new CreateHomeCommand(input.Name, input.Street, input.City, input.ZipCode);
        return await mediator.Send(request);
    }
    
    public async Task<string> RemoveHomeById(Guid homeId)
    {
        logger.LogInformation("Removing home by id: {HomeId}", homeId);
        
        var request = new RemoveHomeCommand(homeId);
        var result = await mediator.Send(request);

        return result.ToString();
    }
}