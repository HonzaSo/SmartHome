using Microsoft.EntityFrameworkCore;
using SmartHome.Infrastructure.Entities;

namespace SmartHome.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Home> Homes => Set<Home>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Device> Devices => Set<Device>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Home>(entity =>
        {
            entity.HasKey(h => h.Id);
            entity.ComplexProperty(h => h.Address); 
        
            entity.HasMany(h => h.Rooms)
                .WithOne(r => r.Home)
                .HasForeignKey(r => r.HomeId);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(r => r.Id);
        
            entity.HasMany(r => r.Devices)
                .WithOne(d => d.Room)
                .HasForeignKey(d => d.RoomId);
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(d => d.Id);
        });
    }
}