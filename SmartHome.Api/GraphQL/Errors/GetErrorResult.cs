using SmartHomeApi.GraphQL.Enums;
using SmartHomeApi.GraphQL.Interfaces;

namespace SmartHomeApi.GraphQL.Errors;

public record GetErrorResult : IGetHomeResult, IGetRoomResult, IGetRoomsResult, IGetDevicesResult
{
    public string Message { get; init; }
    public ErrorCategory Code { get; init; }
    public string EntityName { get; init; }

    public GetErrorResult(string message, ErrorCategory code, string entityName)
    {
        Message = message;
        Code = code;
        EntityName = entityName;
    }
}