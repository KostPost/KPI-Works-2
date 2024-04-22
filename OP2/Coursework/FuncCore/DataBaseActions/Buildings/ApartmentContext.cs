using System.Runtime.InteropServices.JavaScript;
using FuncCore.Buildings;
using Microsoft.EntityFrameworkCore;

namespace FuncCore.DataBaseActions.Buildings;

public class ApartmentContext : DbContext
{
    public DbSet<Apartment> Apartments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=\"Management of the house\";Username=postgres;Password=2025");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the primary key for Apartment
        modelBuilder.Entity<Apartment>()
            .HasKey(a => a.ApartmetId);

        // Configure the relationship between Apartment and Person
        modelBuilder.Entity<Apartment>()
            .HasMany(a => a.Residents)  // Apartment has many Residents
            .WithOne()                  // Residents have one Apartment (assuming there is a foreign key in Person)
            .HasForeignKey(p => p.ApartmentId)  // The foreign key in Person pointing to Apartment
            .IsRequired(false)          // If the foreign key can be null
            .OnDelete(DeleteBehavior.SetNull); // Configure the delete behavior

    }

    public static Apartment? GetApartmentByNumberAndBuildingId(ApartmentContext context, int number, long buildingId)
    {
        return context.Apartments.FirstOrDefault(a => a.ApartmentNumber == number && a.BuildingId == buildingId);
    }

    public static void AddApartment(ApartmentContext context, int number, long roomCount, long floor, long buildingId,
        decimal costByOneM, Building building)
    {
        var newApartment = new Apartment
        {
            ApartmentNumber = number,
            RoomCount = roomCount,
            Floor = floor,
            BuildingId = buildingId,
            CostPerSquareMeter = costByOneM
        };

        if (floor > building.NumberOfFloors)
        {
            Console.WriteLine("Error: The floor of the apartment cannot exceed the maximum floor of the building.");
            return;
        }

        context.Apartments.Add(newApartment);
        context.SaveChanges();
        Console.WriteLine($"Apartment with ID {newApartment.ApartmetId} added successfully.");
    }

    public static Apartment? FindApartmentById (ApartmentContext context, long id)
    {
        return context.Apartments.FirstOrDefault(b => b.ApartmetId == id);

    }


    // public static List<Apartment> FindApartmentsByBuildingId(ApartmentContext context, long buildingId)
    // {
    //     var apartments = context.Apartments.Where(a => a.BuildingId == buildingId).ToList();
    //
    //     if (apartments.Any())
    //     {
    //         Console.WriteLine($"Found {apartments.Count} apartments in building ID {buildingId}:");
    //     }
    //     else
    //     {
    //         Console.WriteLine($"No apartments found in building ID {buildingId}.");
    //     }
    //
    //     return apartments;
    // }
    

    public static List<Apartment> FindApartmentsByBuildingId(BuildingsContext context, long buildingId)
    {
        return context.Apartments
            .Where(a => a.BuildingId == buildingId)
            .Include(a => a.Rooms)          // Including rooms information if needed
            .Include(a => a.ApartmentOwner) // Including owner details if needed
            .Include(a => a.Residents)      // Including residents details if needed
            .ToList();
    }

    // public static void UpdateCostPerSquareMeter(Apartment apartment, decimal newCost)
    // {
    //     apartment.CostPerSquareMeter = newCost;
    //
    //     using (var dbContext = new ApartmentContext()) 
    //     {
    //         dbContext.Entry(apartment).State = EntityState.Modified;
    //         dbContext.SaveChanges();
    //     }
    //
    //     Console.WriteLine($"Cost per square meter for apartment {apartment.ApartmentNumber} updated successfully.");
    // }
}