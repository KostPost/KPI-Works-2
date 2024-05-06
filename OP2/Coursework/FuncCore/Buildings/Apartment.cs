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
    public List<Tenant> Tenants { get; set; }  = new List<Tenant>();
    public List<UtilityExpenses> UtilityExpenses { get; set; } = new List<UtilityExpenses>();
    public List<RepairExpense> RepairExpenses { get; set; }  = new List<RepairExpense>();
    public bool IsOccupied { get; set; }

    public Apartment()
    {
        
    }
    
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
        RepairExpenses.Add(new RepairExpense { Cost = cost, Description = description});
    }

    public void AddRoom()
    {
        ApartmentInternal privateMethods = new ApartmentInternal(this);

        Console.WriteLine("Enter room details:");
        Console.WriteLine("Room number:");
        int roomNumber;
        while (!int.TryParse(Console.ReadLine(), out roomNumber))
        {
            Console.WriteLine("Invalid input. Please enter a valid room number:");
        }

        Console.WriteLine("Area:");
        double area;
        while (!double.TryParse(Console.ReadLine(), out area))
        {
            Console.WriteLine("Invalid input. Please enter a valid area:");
        }

        privateMethods.AddRoom(roomNumber, area);

        Console.WriteLine("Room added successfully to the apartment.");
    }
    
    public void UpdateCostPerSquareMeter()
    {
        Console.WriteLine("Enter new cost per square meter:");
        var newCostInput = Console.ReadLine();
        if (decimal.TryParse(newCostInput, out decimal newCost))
        {
            CostPerSquareMeter = newCost;
            Console.WriteLine("Cost per square meter updated successfully.");
        }
        else
        {
            Console.WriteLine("Invalid input for cost per square meter.");
        }
    }
    
    public void AddTenant()
    {
        Console.Write("Enter resident's name: ");
        string? residentName = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(residentName))
        {
            Tenant newTenant = new Tenant { FullName = residentName };
            Tenants.Add(newTenant);
            Console.WriteLine($"Tenant '{residentName}' added to apartment {ApartmentNumber}.");
        }
        else
        {
            Console.WriteLine("Resident's name cannot be empty. Try again.");
        }
    }    
    
    public void UpdateTenantInformation()
    {
        Console.WriteLine("List of current residents/tenants:");
        PrintTenants();

        Console.Write("Enter the full name of the resident/tenant to update: ");
        string? residentNameToUpdate = Console.ReadLine()?.Trim();

        Tenant? tenantToUpdate = Tenants.FirstOrDefault(t =>
            t.FullName.Equals(residentNameToUpdate, StringComparison.OrdinalIgnoreCase));

        if (tenantToUpdate != null)
        {
            Console.WriteLine($"Updating information for {residentNameToUpdate}:");
            Console.Write("Enter new full name: ");
            string? newFullName = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(newFullName))
            {
                tenantToUpdate.FullName = newFullName;
                Console.WriteLine("Full name updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid input for full name.");
            }

            Console.WriteLine($"Information for {residentNameToUpdate} updated successfully.");
        }
        else
        {
            Console.WriteLine(
                $"Resident/tenant '{residentNameToUpdate}' not found in the apartment.");
        }
    }
    
    public void RemoveTenant()
    {
        Console.WriteLine("List of current residents/tenants:");
        PrintTenants();
        Console.Write("Enter the full name of the resident/tenant to remove: ");
        string? residentNameToRemove = Console.ReadLine()?.Trim();

        Tenant? tenantToRemove = Tenants.FirstOrDefault(t =>
            t.FullName.Equals(residentNameToRemove, StringComparison.OrdinalIgnoreCase));

        if (tenantToRemove != null)
        {
            Tenants.Remove(tenantToRemove);

            if (Tenants.Count == 0)
                IsOccupied = false;

            Console.WriteLine($"Resident/tenant '{residentNameToRemove}' removed from the apartment.");
        }
        else
        {
            Console.WriteLine($"Resident/tenant '{residentNameToRemove}' not found in the apartment.");
        }
    }
    
    public void AddUtilityExpensesAndCalculateRent()
    {
        if (!RentTermStart.Equals(DateTime.MinValue) && !RentTermEnd.Equals(DateTime.MinValue))
        {
            Console.WriteLine($"Current rent term: {RentTermStart:d} - {RentTermEnd:d}");
            
            UtilityExpenses utilityExpenses = new UtilityExpenses();
                    utilityExpenses.AddUtilityExpensesForMonth(this);
        }
        else
        {
            Console.WriteLine("Rent term not set for the current apartment.");
        }
    }
    
    
    public void UpdateRentTermDates()
    {
        var apartmentInternal = new ApartmentInternal(this);
        apartmentInternal.UpdateRentTermDates(this);
    }
    
    public void AddRepairExpense()
    {
        var apartmentInternal = new ApartmentInternal(this);
        apartmentInternal.AddRepairExpense(this);
    }
    public void ViewUtilityExpenses()
    {
        var apartmentInternal = new ApartmentInternal(this);
        apartmentInternal.ViewUtilityExpenses(this);
    }
    
    public void AddLandlord()
    {
        Console.Write("Enter landlord's name: ");
        string? landlordName = Console.ReadLine()?.Trim();

        if (!string.IsNullOrWhiteSpace(landlordName))
        {
            LandLord newLandLord = new LandLord
            {
                FullName = landlordName,
                ApartmentNumber = ApartmentNumber
            };

            ApartmentOwner = newLandLord;

            Console.WriteLine($"Landlord '{landlordName}' added to apartment {ApartmentNumber}.");
        }
        else
        {
            Console.WriteLine("Landlord's name cannot be empty. Try again.");
        }
    }
    
    
    public static Apartment? GetApartmentByNumber(List<Apartment> apartments)
    {
        Console.WriteLine("Enter the apartment number:");
        var apartmentNumberInput = Console.ReadLine();
        if (int.TryParse(apartmentNumberInput, out int apartmentNumber))
        {
            foreach (var apartment in apartments)
            {
                if (apartment.ApartmentNumber == apartmentNumber)
                {
                    return apartment;
                }
            }
            Console.WriteLine($"Apartment with number {apartmentNumber} not found.");
        }
        else
        {
            Console.WriteLine("Invalid apartment number input.");
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

    public decimal CalculateRent(int month)
    {
        double totalArea = CalculateTotalArea();
        decimal totalRent = CostPerSquareMeter * (decimal)totalArea * month;
        return totalRent;
    }

    public void CloseRepairExpense()
    {
        Console.WriteLine($"Apartment Number: {ApartmentNumber}");
        Console.WriteLine("Select a repair expense to close:\n");

        for (int i = 0; i < RepairExpenses.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Description: {RepairExpenses[i].Description}, Cost: {RepairExpenses[i].Cost:C}");
        }

        Console.WriteLine("\nEnter the number of the repair expense to close:");
        int selectedIndex = int.Parse(Console.ReadLine()) - 1;

        if (selectedIndex >= 0 && selectedIndex < RepairExpenses.Count)
        {
            RepairExpenses.RemoveAt(selectedIndex);
            Console.WriteLine("Repair expense closed successfully.");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
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