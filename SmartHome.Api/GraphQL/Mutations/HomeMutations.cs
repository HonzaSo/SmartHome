using MediatR;
using SmartHome.Application.Commands;
using SmartHomeApi.GraphQL.Types;

namespace SmartHomeApi.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class HomeMutations (IMediator mediator, ILogger<HomeMutations> logger)
{
    public async Task<Guid> CreateHome(HomeType input)
    {
        logger.LogInformation("Creating home {HomeName}", input.Name);
        
        var request = new CreateHomeCommand(input.Name, input.Street, input.City, input.ZipCode);
        return await mediator.Send(request);
    }
}
    
