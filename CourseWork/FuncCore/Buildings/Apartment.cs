using System.Globalization;
using System.Text;
using FuncCore.Persons;

namespace FuncCore;

public class Apartment
{
    public int ApartmentNumber { get; set; }
    public long RoomCount { get; set; }
    public long Floor { get; set; }
    public double CostPerSquareMeter { get; set; }
    public DateTime RentTermStart { get; set; }
    public DateTime RentTermEnd { get; set; }
    public LandLord? LandLord { get; set; }
    public List<Room> Rooms { get; set; } 
    public List<Tenant> Tenants { get; set; } 
    public List<UtilityExpense> UtilityExpenses { get; set; } 
    public List<RepairExpense> RepairExpenses { get; set; }
    public bool IsOccupied { get; set; }

    public Apartment()
    {
        Rooms = new List<Room>();
        Tenants = new List<Tenant>();
        UtilityExpenses = new List<UtilityExpense>();
        RepairExpenses = new List<RepairExpense>();
    }

   

    public void AddRepairExpense()
    {
       RepairExpense.AddRepairExpense(this);
    }

    public void CloseRepairExpense()
    {
        RepairExpense.CloseRepairExpense(this);
    }

    public void PrintRepairExpenses()
    {
        RepairExpense.PrintRepairExpenses(RepairExpenses);
    }
    public void UpdateRentDate()
    {
        try
        {
        Console.WriteLine("Enter the new Rent Term Start Date (Format: dd-MM-yyyy):");
        string inputStartDate = Console.ReadLine();

        if (!DateTime.TryParseExact(inputStartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newRentStartDate))
        {
            Console.WriteLine("Invalid start date format. Please enter a valid date in the format dd-MM-yyyy.");
            return;
        }

        Console.WriteLine("Enter the new Rent Term End Date (Format: dd-MM-yyyy):");
        string inputEndDate = Console.ReadLine();

        if (!DateTime.TryParseExact(inputEndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newRentEndDate))
        {
            Console.WriteLine("Invalid end date format. Please enter a valid date in the format dd-MM-yyyy.");
            return;
        }

        RentTermStart = newRentStartDate;
        RentTermEnd = newRentEndDate;
        Console.WriteLine("Rent dates updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    public void CalculateRentForPeriod()
    {
        Console.WriteLine("Enter the start date of the rent period (Format: dd-MM-yyyy):");
        DateTime startPeriod = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

        Console.WriteLine("Enter the end date of the rent period (Format: dd-MM-yyyy):");
        DateTime endPeriod = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

        double totalRent = CalculateRentForPeriod(startPeriod, endPeriod);

        Console.WriteLine($"The total rent for the period from {startPeriod:dd-MM-yyyy} to {endPeriod:dd-MM-yyyy} is: {totalRent:C}");
    }
    public void AddUtilityCosts()
    {
        UtilityExpense.AddUtilityCosts(this);

    }
    public void AddLandlordToApartment(Building building)
    {
        LandLord.AddLandlordToApartment(this, building);
    }
    public void RemoveLandlordFromApartment(Building building)
    {
        LandLord.RemoveLandlordFromApartment(this,building);
    }
    public void RemoveTenant()
    {
        Tenant.RemoveTenantFromApartment(this);
    }
    public void DisplayAllTenants()
    {
       Tenant.DisplayAllTenants(this);
    }
    public void UpdateTenantInformation()
    {
        Tenant.UpdateTenantInformation(this);
    }
    public void RegisterNewTenant()
    {
        Tenant.RegisterNewTenant(this);
    }
    public void AddRoom()
    {
        Room.AddRoom(this);
    }
    public void UpdateCost()
    {
        try
        {
            Console.WriteLine("Enter the new cost per one meter:");
            if (double.TryParse(Console.ReadLine(), out double newCost))
            {
                CostPerSquareMeter = newCost;
                Console.WriteLine("Cost updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid floating point number.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    
    public void PrintApartmentDetails()
    {
        Console.WriteLine("-----------");

        var builder = new StringBuilder();
        builder.AppendLine($"Apartment Number: {ApartmentNumber}");
        builder.AppendLine($"Room Count: {RoomCount}");
        builder.AppendLine($"Floor: {Floor}");
        builder.AppendLine($"Cost Per Square Meter: {CostPerSquareMeter:C}");
        builder.AppendLine($"Rent Term Start: {RentTermStart:d}");
        builder.AppendLine($"Rent Term End: {RentTermEnd:d}");
        builder.AppendLine($"Is Occupied: {IsOccupied}");

        if (LandLord != null)
        {
            builder.AppendLine("Apartment Owner:");
            builder.AppendLine($"  Name: {LandLord.FullName}\n");
        }

        if (Tenants?.Any() == true)
        {
            builder.AppendLine("Tenants:");
            foreach(var tenant in Tenants)
            {
                builder.AppendLine($"  Name: {tenant.FullName}\n");
            }
        }

        if(UtilityExpenses.Count != 0)
        {
            UtilityExpense.PrintUtilityExpenses(UtilityExpenses);
        }

        if(RepairExpenses.Count != 0)
        {
            RepairExpense.PrintRepairExpenses(RepairExpenses);
        }

        Console.Write(builder.ToString());
        Console.WriteLine("-----------");

    }
    public static void PrintApartmentsDetails(List<Apartment> apartments)
    {
        foreach (var apartment in apartments)
        {
            Console.WriteLine("-----------");

            var builder = new StringBuilder();
        
            builder.AppendLine($"Apartment Number: {apartment.ApartmentNumber}");
            builder.AppendLine($"Room Count: {apartment.RoomCount}");
            builder.AppendLine($"Floor: {apartment.Floor}");
            builder.AppendLine($"Cost Per Square Meter: {apartment.CostPerSquareMeter:C}");
            builder.AppendLine($"Rent Term Start: {apartment.RentTermStart:d}");
            builder.AppendLine($"Rent Term End: {apartment.RentTermEnd:d}");
            builder.AppendLine($"Is Occupied: {apartment.IsOccupied}");

            if (apartment.LandLord != null)
            {
                builder.AppendLine("Apartment Owner:");
                builder.AppendLine($"  Name: {apartment.LandLord.FullName}");
            }

            if (apartment.Tenants?.Any() == true)
            {
                builder.AppendLine("Tenants:");
                foreach(var tenant in apartment.Tenants)
                {
                    builder.AppendLine($"  Name: {tenant.FullName}");
                }
            }

            if(apartment.UtilityExpenses.Count != 0)
            {
                UtilityExpense.PrintUtilityExpenses(apartment.UtilityExpenses);
            }

            if(apartment.RepairExpenses.Count != 0)
            {
                RepairExpense.PrintRepairExpenses(apartment.RepairExpenses);
            }

            Console.Write(builder.ToString());
            Console.WriteLine("-----------");
        }
    }
    
    private double CalculateRentForPeriod(DateTime startPeriod, DateTime endPeriod)
    {
        int monthsCount = ((endPeriod.Year - startPeriod.Year) * 12) + endPeriod.Month - startPeriod.Month;

        double totalRent = CalculateTotalArea() * (double)CostPerSquareMeter * monthsCount;

        return totalRent;
    }
    private double CalculateTotalArea()
    {
        return Rooms.Sum(room => room.Area);
    }
}