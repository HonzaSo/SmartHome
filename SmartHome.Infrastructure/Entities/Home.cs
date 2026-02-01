namespace SmartHome.Infrastructure.Entities;

public class Home
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
    
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}