namespace SmartHome.Infrastructure.Configurations;

public class Database
{
    public required string Server { get; set; }
    public required int Port { get; set; }
    public required string User { get; set; }
    public required string Password { get; set; }
    public required string DatabaseName { get; set; }
    
    public string ConnectionString => $"Host={Server};Port={Port};Database={DatabaseName};Username={User};Password={Password};";
}