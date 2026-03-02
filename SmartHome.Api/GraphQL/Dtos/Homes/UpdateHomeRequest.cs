namespace SmartHomeApi.GraphQL.Dtos.Homes;

public class UpdateHomeRequest
{
    public string? Name { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
}

