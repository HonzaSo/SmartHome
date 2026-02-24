namespace SmartHomeApi.GraphQL.Interfaces;

public interface ICreateDeviceResult
{
}

public record CreateDeviceSuccess(Guid Id) : ICreateDeviceResult;