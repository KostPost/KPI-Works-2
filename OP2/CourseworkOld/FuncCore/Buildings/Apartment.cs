using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
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
    
    public void AddRoom()
    {
        try
        {
            Console.WriteLine("Enter room details:");
        
            int roomNumber;
            Console.WriteLine("Room number:");
            while (!int.TryParse(Console.ReadLine(), out roomNumber))
            {
                Console.WriteLine("Invalid input. Please enter a valid room number:");
            }

            double area;
            Console.WriteLine("Area:");
            while (!double.TryParse(Console.ReadLine(), out area))
            {
                Console.WriteLine("Invalid input. Please enter a valid area:");
            }

            AddRoom(roomNumber, area);

            Console.WriteLine("Room added successfully to the apartment.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void UpdateCostPerSquareMeter()
    {
        try
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
                throw new ArgumentException("Invalid input for cost per square meter.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void AddTenant()
    {
        try
        {
            Console.Write("Enter resident's name: ");
            string? residentName = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(residentName))
            {
                Tenant newTenant = new Tenant { FullName = residentName };
                newTenant.ApartmentNumber = ApartmentNumber;
                Tenants.Add(newTenant);
                Console.WriteLine($"Tenant '{residentName}' added to apartment {ApartmentNumber}.");
            }
            else
            {
                throw new ArgumentException("Resident's name cannot be empty.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }


    public void UpdateTenantInformation()
    {
        Console.WriteLine("List of current residents/tenants:");
        PrintTenants();

        Console.Write("Enter the full name of the resident/tenant to update: ");
        string? residentNameToUpdate = Console.ReadLine()?.Trim();

        Tenant? tenantToUpdate = Tenants.FirstOrDefault(t => t.FullName.Equals(residentNameToUpdate, StringComparison.OrdinalIgnoreCase));

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
            Console.WriteLine($"Resident/tenant '{residentNameToUpdate}' not found in the apartment.");
        }
    }

    public void RemoveTenant()
    {
        Console.WriteLine("List of current residents/tenants:");
        PrintTenants();
        Console.Write("Enter the full name of the resident/tenant to remove: ");
        string? residentNameToRemove = Console.ReadLine()?.Trim();

        Tenant? tenantToRemove = Tenants.FirstOrDefault(t => t.FullName.Equals(residentNameToRemove, StringComparison.OrdinalIgnoreCase));

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
        try
        {
            if (RentTermStart == DateTime.MinValue || RentTermEnd == DateTime.MinValue)
            {
                Console.WriteLine("Rent term not set for the current apartment.");
                return;
            }

            Console.WriteLine($"Current rent term: {RentTermStart:d} - {RentTermEnd:d}");

            UtilityExpenses utilityExpenses = new UtilityExpenses();
            utilityExpenses.AddUtilityExpensesForMonth(this);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void UpdateRentTermDates()
    {
        try
        {
            Console.Write("Enter the new rent start date (yyyy-MM-dd): ");
            if (!TryParseDate(Console.ReadLine(), out DateTime newRentStartDate))
            {
                Console.WriteLine("Invalid date format. Please enter the date in yyyy-MM-dd format.");
                return;
            }

            Console.Write("Enter the new rent end date (yyyy-MM-dd): ");
            if (!TryParseDate(Console.ReadLine(), out DateTime newRentEndDate))
            {
                Console.WriteLine("Invalid date format. Please enter the date in yyyy-MM-dd format.");
                return;
            }

            RentTermStart = newRentStartDate;
            RentTermEnd = newRentEndDate;

            Console.WriteLine("Rent start and end dates updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void AddRepairExpense()
    {
        try
        {
            Console.Write("Enter the cost of repair: ");
            decimal repairCost;
            while (!decimal.TryParse(Console.ReadLine(), out repairCost) || repairCost < 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid repair cost:");
            }

            string repairDescription;
            do
            {
                Console.Write("Enter the description of repair: ");
                repairDescription = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(repairDescription));

            RepairExpenses.Add(new RepairExpense { Cost = repairCost, Description = repairDescription });
            Console.WriteLine("Repair expenses added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void ViewUtilityExpenses()
    {
        try
        {
            if (RentTermStart == DateTime.MinValue || RentTermEnd == DateTime.MinValue)
            {
                Console.WriteLine("Rent term not set for the current apartment.");
                return;
            }

            Console.WriteLine($"Current rent term: {RentTermStart:d} - {RentTermEnd:d}");
            UtilityExpenses utilityExpenses = new UtilityExpenses();
            utilityExpenses.ViewUtilityExpensesForMonth(this);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }


    public void AddLandlord()
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }


    public static Apartment? GetApartmentByNumber(List<Apartment> apartments)
    {
        try
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
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return null;
    }


    


    public void CloseRepairExpense()
    {
        try
        {
            Console.WriteLine($"Apartment Number: {ApartmentNumber}");
            Console.WriteLine("Select a repair expense to close:\n");

            for (int i = 0; i < RepairExpenses.Count; i++)
            {
                Console.WriteLine(
                    $"{i + 1}. Description: {RepairExpenses[i].Description}, Cost: {RepairExpenses[i].Cost:C}");
            }

            Console.WriteLine("\nEnter the number of the repair expense to close:");
            if (int.TryParse(Console.ReadLine(), out int selectedIndex))
            {
                selectedIndex--; 

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
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
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
    
    
    
    public decimal CalculateRentForPeriod(DateTime startDate, DateTime endDate)
    {
        DateTime rentStartDate = RentTermStart;
        DateTime rentEndDate = RentTermEnd;

        if (rentEndDate <= startDate || rentStartDate >= endDate.AddDays(1))
        {
            return 0;
        }

        DateTime periodStartDate = startDate > rentStartDate ? startDate : rentStartDate;
        DateTime periodEndDate = endDate < rentEndDate ? endDate : rentEndDate;

        int months = 0;
        DateTime currentDate = periodStartDate;

        while (currentDate < periodEndDate)
        {
            currentDate = currentDate.AddMonths(1);
            months++;
        }

        decimal totalRentForPeriod = CalculateRent(months);

        return totalRentForPeriod;
    }

    
    public decimal CalculateRent(int month)
    {
        try
        {
            if (month <= 0)
            {
                throw new ArgumentException("Month parameter must be a positive integer.");
            }

            double totalArea = CalculateTotalArea();
            decimal totalRent = CostPerSquareMeter * (decimal)totalArea * month;
            return totalRent;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return 0;
        }
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
    private void AddRoom(int roomNumber, double area)
    {
        Rooms.Add(new Room { RoomNumber = roomNumber, Area = area, ApartmentId = ApartmentNumber });
    }
    private bool TryParseDate(string input, out DateTime result)
    {
        return DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
    }


}