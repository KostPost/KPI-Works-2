using System.ComponentModel.DataAnnotations.Schema;
using FuncCore.Buildings;

namespace FuncCore.DataBaseActions;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class BuildingsContext : DbContext
{
    public DbSet<Building> buildings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=\"Management of the house\";Username=postgres;Password=2025");
    }
    
    public static Building? FindBuildingById(BuildingsContext context, long id)
    {
        return context.buildings.FirstOrDefault(b => b.BuildingId == id);
    }
    public static Building? FindBuildingByName(BuildingsContext context, string name)
    {
        return context.buildings.FirstOrDefault(b => b.BuildingName == name);
    }



    public static void AddBuilding()
    {
        using (var context = new BuildingsContext())
        {
            var newBuilding = new Building
            {
                BuildingName = "build 1",
                NumberOfFloors = 10
            };

            context.buildings.Add(newBuilding);

            context.SaveChanges();

            Console.WriteLine($"Building with ID {newBuilding.BuildingId} added successfully.");
        }
    }
}