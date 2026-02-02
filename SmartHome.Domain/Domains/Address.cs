namespace SmartHome.Domain.Domains;

public record Address
{
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
}