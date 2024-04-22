using FuncCore.Buildings;
using Microsoft.EntityFrameworkCore;

namespace FuncCore.DataBaseActions;

public class RoomContext : DbContext
{
    
    public DbSet<Room> Rooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=\"Management of the house\";Username=postgres;Password=2025");
    }
    
}