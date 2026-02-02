namespace SmartHome.Domain.Domains;

public class Home
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
}