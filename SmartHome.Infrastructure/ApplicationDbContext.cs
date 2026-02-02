using Microsoft.EntityFrameworkCore;
using SmartHome.Infrastructure.Entities;

namespace SmartHome.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<HomeEntity> Homes => Set<HomeEntity>();
    public DbSet<RoomEntity> Rooms => Set<RoomEntity>();
    public DbSet<DeviceEntity> Devices => Set<DeviceEntity>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<HomeEntity>(entity =>
        {
            entity.HasKey(h => h.Id);
            entity.ComplexProperty(h => h.Address); 
        
            entity.HasMany(h => h.Rooms)
                .WithOne(r => r.HomeEntity)
                .HasForeignKey(r => r.HomeId);
        });

        modelBuilder.Entity<RoomEntity>(entity =>
        {
            entity.HasKey(r => r.Id);
        
            entity.HasMany(r => r.Devices)
                .WithOne(d => d.RoomEntity)
                .HasForeignKey(d => d.RoomId);
        });

        modelBuilder.Entity<DeviceEntity>(entity =>
        {
            entity.HasKey(d => d.Id);
        });
    }
}