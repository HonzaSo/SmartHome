namespace SmartHomeApi.GraphQL.Dtos.Homes;

public class HomeTypeRequest
{
    public required string Name { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string ZipCode { get; set; }
}