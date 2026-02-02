using MediatR;
using SmartHome.Application.Commands;
using SmartHomeApi.GraphQL.Types;

namespace SmartHomeApi.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class HomeMutations
{
    public async Task<Guid> CreateHome([Service] IMediator mediator, HomeType input)
    {
        var request = new CreateHomeCommand(input.Name, input.Street, input.City, input.ZipCode);
        return await mediator.Send(request);
    }
}
    
