using FuncCore.Buildings;
using Microsoft.EntityFrameworkCore;

namespace FuncCore.DataBaseActions;

public class ApartmentContext : DbContext
{
    public DbSet<Apartment> apartments  { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=\"Management of the house\";Username=postgres;Password=2025");
    }
    
    public static Apartment? GetApartmentByNumberAndBuildingId(ApartmentContext context, int number, long buildingId)
    {
        return context.apartments.FirstOrDefault(a => a.Number == number && a.BuildingId == buildingId);
    }
    public static void AddApartment(ApartmentContext context, int number, long roomCount, long floor, long buildingId,decimal costByOneM , Building building)
    {
        
        var newApartment = new Apartment
        {
            Number = number,
            RoomCount = roomCount,
            Floor = floor,
            BuildingId = buildingId,
            CostByOneM = costByOneM
        };

        if (floor > building.NumberOfFloors)
        {
            Console.WriteLine("Error: The floor of the apartment cannot exceed the maximum floor of the building.");
            return;
        }

        context.apartments.Add(newApartment); 
        context.SaveChanges();
        Console.WriteLine($"Apartment with ID {newApartment.ApartmetId} added successfully.");
    }


    
    public static List<Apartment> FindApartmentsByBuildingId(ApartmentContext context, long buildingId)
    {
        var apartments = context.apartments.Where(a => a.BuildingId == buildingId).ToList();
    
        if (apartments.Any())
        {
            Console.WriteLine($"Found {apartments.Count} apartments in building ID {buildingId}:");

        }
        else
        {
            Console.WriteLine($"No apartments found in building ID {buildingId}.");
        }

        return apartments;
    }




}