using SmartHomeApi.GraphQL.Enums;

namespace SmartHomeApi.GraphQL.Errors;

public class GetErrorResult
{
    public string Message { get; init; }
    public ErrorCategory Code { get; init; }
    public string EntityName { get; init; }

    public GetErrorResult(string entityName, ErrorCategory errorCode, string id)
    {
        EntityName = entityName;
        Code = errorCode;
        Message = $"{entityName} with ID {id} was not found.";
    }
}