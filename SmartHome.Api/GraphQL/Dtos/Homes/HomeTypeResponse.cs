namespace SmartHomeApi.GraphQL.Dtos.Homes;

public class GetHomeType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
}