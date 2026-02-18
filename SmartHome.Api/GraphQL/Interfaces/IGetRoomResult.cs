using SmartHomeApi.GraphQL.Enums;

namespace SmartHomeApi.GraphQL.Interfaces;

public interface IGetRoomResult
{
}

public record GetErrorResult(string Message, ErrorCategory Code, string EntityName) : IGetRoomResult;