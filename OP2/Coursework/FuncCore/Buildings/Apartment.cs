using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FuncCore.Persons;

namespace FuncCore.Buildings;

public class Apartment
{
    public int ApartmentNumber { get; set; }
    public long RoomCount { get; set; }
    public long Floor { get; set; }
    public long BuildingId { get; set; }
    public decimal? CostPerSquareMeter { get; set; }
    public DateTime RentTermStart { get; set; }
    public DateTime RentTermEnd { get; set; }

    public Resident ApartmentOwner { get; set; } 

    public List<Room> Rooms { get; set; } = new List<Room>();
    public List<Tenant> Tenants { get; set; } = new List<Tenant>();

    
    public double RepairCost { get; set; }

    public bool IsOccupied { get; set; }
    
    public Apartment(int apartmentNumber, long roomCount, long floor, long buildingId, decimal? costPerSquareMeter)
    {
        ApartmentNumber = apartmentNumber;
        RoomCount = roomCount;
        Floor = floor;
        BuildingId = buildingId;
        CostPerSquareMeter = costPerSquareMeter;
    }
    
    public void AddTenant(Tenant newTenant)
    {
        Tenants ??= new List<Tenant>();
        Tenants.Add(newTenant);
    }
    
    public static Apartment? GetApartmentByNumber(int apartmentNumber, List<Apartment> apartments)
    {
        foreach (var apartment in apartments)
        {
            if (apartment.ApartmentNumber == apartmentNumber)
            {
                return apartment;
            }
        }
        return null;
    }


    public void PrintApartmentDetails()
    {
        Console.WriteLine($"Apartment Number: {ApartmentNumber}");
        Console.WriteLine($"Room Count: {RoomCount}");
        Console.WriteLine($"Floor: {Floor}");
        Console.WriteLine($"Building ID: {BuildingId}");
        Console.WriteLine($"Cost Per Square Meter: {CostPerSquareMeter:C}");
        Console.WriteLine($"Is Occupied: {IsOccupied}");

        if (ApartmentOwner is LandLord landlord && landlord.ApartmentNumber == ApartmentNumber)
        {
            Console.WriteLine("Landlord lives in this apartment.");
        }
        else
        {
            Console.WriteLine($"Rent Term Start: {RentTermStart:d}");
            Console.WriteLine($"Rent Term End: {RentTermEnd:d}");
        }

        Console.WriteLine();
    }



    public void PrintTenants()
    {
        Console.WriteLine("Tenants in Apartment with number: " + ApartmentNumber);
        foreach (Tenant tenant in Tenants)
        {
            Console.WriteLine("Tenant name: " + tenant.FullName);
        }
        Console.WriteLine("");
    }

    public void PrintOwnerDetails()
    {
        
    }


}

