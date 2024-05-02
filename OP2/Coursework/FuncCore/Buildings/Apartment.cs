using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FuncCore.Persons;
using Npgsql.Internal;


namespace FuncCore.Buildings;

public class Apartment
{
    public int ApartmentNumber { get; set; }
    public long RoomCount { get; set; }
    public long Floor { get; set; }
    public long BuildingId { get; set; }
    public decimal CostPerSquareMeter { get; set; }
    public DateTime RentTermStart { get; set; }
    public DateTime RentTermEnd { get; set; }
    public LandLord ApartmentOwner { get; set; } 
    public List<Room> Rooms { get; set; } = new List<Room>();
    public List<Tenant> Tenants { get; set; } = new List<Tenant>();
    public List<UtilityExpenses> UtilityExpenses { get; set; } = new List<UtilityExpenses>();
    public List<RepairExpense> RepairExpenses { get; set; } = new List<RepairExpense>();


    public bool IsOccupied { get; set; }
    
    
    public Apartment(int apartmentNumber, long roomCount, long floor, long buildingId, decimal costPerSquareMeter)
    {
        ApartmentNumber = apartmentNumber;
        RoomCount = roomCount;
        Floor = floor;
        BuildingId = buildingId;
        CostPerSquareMeter = costPerSquareMeter;
    }
    public void AddRepairExpense(decimal cost, string description)
    {
        RepairExpenses.Add(new RepairExpense { Cost = cost, Description = description, Date = DateTime.Now });
    }
    public void AddRoom(int roomNumber, double area)
    {
        Rooms.Add(new Room { RoomNumber = roomNumber, Area = area, ApartmentId = this.ApartmentNumber });
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

    public double CalculateTotalArea()
    {
        double totalArea = 0;
        foreach (var room in Rooms)
        {
            totalArea += room.Area;
        }
        return totalArea;
    }
    public decimal CalculateRent( int month)
    {
        double totalArea = CalculateTotalArea();
        decimal totalRent = CostPerSquareMeter * (decimal)totalArea * month;
        return totalRent;
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
            Console.WriteLine($"Landlord Name: {landlord.FullName}");
            Console.WriteLine($"Landlord Income: {landlord.Income:C}");
        }
        else
        {
            Console.WriteLine($"Rent Term Start: {RentTermStart:d}");
            Console.WriteLine($"Rent Term End: {RentTermEnd:d}");
        }

        decimal totalCost = RepairExpense.CalculateAllRepairExpense(RepairExpenses);
        Console.WriteLine($"Repair Costs: {totalCost:C}");

        Console.WriteLine();
    }


    public void PrintTenants()
    {
        Console.WriteLine($"Tenants in Apartment with number: {ApartmentNumber}");
        foreach (Tenant tenant in Tenants)
        {
            Console.WriteLine($"Tenant Name: {tenant.FullName}");
            Console.WriteLine($"Tenant Phone Number: {tenant.PhoneNumber}");
            Console.WriteLine($"Tenant Email: {tenant.Email}");
            Console.WriteLine($"Emergency Contact: {tenant.EmergencyContact}");
        }
        Console.WriteLine();
    }





}

