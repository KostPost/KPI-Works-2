using System.Globalization;

namespace FuncCore.Buildings;

internal class ApartmentInternal
{
    private readonly Apartment _apartment;

    public ApartmentInternal(Apartment apartment)
    {
        _apartment = apartment;
    }

    public void AddRoom(int roomNumber, double area)
    {
        _apartment.Rooms.Add(new Room { RoomNumber = roomNumber, Area = area, ApartmentId = _apartment.ApartmentNumber });
    }
    
    public void UpdateRentTermDates(Apartment currentApartment)
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

        currentApartment.RentTermStart = newRentStartDate;
        currentApartment.RentTermEnd = newRentEndDate;

        Console.WriteLine("Rent start and end dates updated successfully.");
    }

    private bool TryParseDate(string input, out DateTime result)
    {
        return DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
    }
    
    public void AddRepairExpense(Apartment currentApartment)
    {
        Console.Write("Enter the cost of repair: ");
        decimal repairCost;
        while (!decimal.TryParse(Console.ReadLine(), out repairCost) || repairCost < 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid repair cost:");
        }

        Console.Write("Enter the description of repair: ");
        string? repairDescription = Console.ReadLine();

        currentApartment.AddRepairExpense(repairCost, repairDescription);
        Console.WriteLine("Repair expenses added successfully.");
    }
    
    public void ViewUtilityExpenses(Apartment currentApartment)
    {
        if (!currentApartment.RentTermStart.Equals(DateTime.MinValue) &&
            !currentApartment.RentTermEnd.Equals(DateTime.MinValue))
        {
            Console.WriteLine($"Current rent term: {currentApartment.RentTermStart:d} - {currentApartment.RentTermEnd:d}");
            UtilityExpenses.ViewUtilityExpensesForMonth(currentApartment);
        }
        else
        {
            Console.WriteLine("Rent term not set for the current apartment.");
        }
    }

    
    
}