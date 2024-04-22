using System.ComponentModel.DataAnnotations.Schema;
using FuncCore.Buildings;

namespace FuncCore.DataBaseActions;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class BuildingsContext : DbContext
{
    public DbSet<Building> Buildings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=\"Management of the house\";Username=postgres;Password=2025");
    }
    
    public static Building? FindBuildingById(BuildingsContext context, long id)
    {
        return context.Buildings.FirstOrDefault(b => b.BuildingId == id);
    }
    public static Building? FindBuildingByName(BuildingsContext context, string name)
    {
        return context.Buildings.FirstOrDefault(b => b.BuildingName == name);
    }



    public static void AddBuilding(string buildingName, int numberOfFloors)
    {
        using (var context = new BuildingsContext())
        {
            var newBuilding = new Building
            {
                BuildingName = buildingName,
                NumberOfFloors = numberOfFloors
            };

            context.Buildings.Add(newBuilding);

            context.SaveChanges();

            Console.WriteLine($"Building '{buildingName}' with ID {newBuilding.BuildingId} added successfully.");
        }
    }

}