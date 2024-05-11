using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace FuncCore.Buildings;

public class UtilityExpenses
{
    public long ApartmentId { get; set; }

    public decimal RentCost { get; set; }

    public decimal AllUtilityExpenses { get; set; }

    public decimal HeatingCost { get; set; }

    public decimal WaterCost { get; set; }

    public decimal ElectricityCost { get; set; }

    public decimal GasCost { get; set; }

    public decimal CleaningCost { get; set; }

    public decimal ManagementCost { get; set; }

    public decimal TrashRemovalCost { get; set; }

    public decimal InternetTvPhoneCost { get; set; }

    public DateTime UtilityExpensesMonth { get; set; }
    
    private static decimal PromptForDecimal(string costName)
    {
        decimal cost;
        Console.Write($"{costName}: ");
        while (!decimal.TryParse(Console.ReadLine(), out cost) || cost < 0)
        {
            Console.WriteLine($"Invalid input. Please enter a valid {costName.ToLower()}:");
        }
        return cost;
    }

    private static DateTime PromptForMonthYear()
    {
        DateTime date;
        while (!DateTime.TryParseExact(Console.ReadLine(), "MM/yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
        {
            Console.WriteLine("Invalid input. Please enter the month and year in the format MM/yyyy:");
        }
        return date;
    }
    
    public void ViewUtilityExpensesForMonth(Apartment currentApartment)
    {
        DateTime rentTermStart = currentApartment.RentTermStart;
        DateTime rentTermEnd = currentApartment.RentTermEnd;

        Console.WriteLine($"Current rent term: {rentTermStart:d} - {rentTermEnd:d}");

        Console.Write("Enter the month and year (MM/yyyy) to view utility expenses: ");
        DateTime monthToView = PromptForMonthYear();

        var expensesForMonth = currentApartment.UtilityExpenses.Where(e => e.UtilityExpensesMonth.Month == monthToView.Month && e.UtilityExpensesMonth.Year == monthToView.Year).ToList();

        if (expensesForMonth.Count == 0)
        {
            Console.WriteLine($"No utility expenses found for {monthToView:MMMM yyyy}.");
            return;
        }

        Console.WriteLine($"Utility Expenses for {monthToView:MMMM yyyy}");
        Console.WriteLine("----------------------------");

        foreach (var expenses in expensesForMonth)
        {
            Console.WriteLine($"Electricity Cost: {expenses.ElectricityCost:C}");
            Console.WriteLine($"Water Cost: {expenses.WaterCost:C}");
            Console.WriteLine($"Heating Cost: {expenses.HeatingCost:C}");
            Console.WriteLine($"Gas Cost: {expenses.GasCost:C}");
            Console.WriteLine($"Cleaning Cost: {expenses.CleaningCost:C}");
            Console.WriteLine($"Management Cost: {expenses.ManagementCost:C}");
            Console.WriteLine($"Trash Removal Cost: {expenses.TrashRemovalCost:C}");
            Console.WriteLine($"Internet, TV, and Phone Cost: {expenses.InternetTvPhoneCost:C}");
            Console.WriteLine($"Total Utility Expenses: {expenses.AllUtilityExpenses:C}");
            Console.WriteLine("----------------------------");
        }
    }

    public void AddUtilityExpensesForMonth(Apartment currentApartment)
    {
        DateTime rentTermStart = currentApartment.RentTermStart;
        DateTime rentTermEnd = currentApartment.RentTermEnd;

        Console.WriteLine($"Current rent term: {rentTermStart:d} - {rentTermEnd:d}");

        Console.Write("Enter the month and year (MM/yyyy): ");
        DateTime inputMonthYear = PromptForMonthYear();

        DateTime inputDate = new DateTime(inputMonthYear.Year, inputMonthYear.Month, 1);

        if (inputDate < rentTermStart || inputDate > rentTermEnd)
        {
            Console.WriteLine($"Invalid date selection. Please choose a date between {rentTermStart:MMMM yyyy} and {rentTermEnd:MMMM yyyy}.");
            return;
        }

        Console.WriteLine($"Enter utility costs for {inputDate:MMMM yyyy}:");

        Console.Write("Electricity cost: ");
        decimal electricityCost = PromptForDecimal("Electricity cost");

        Console.Write("Water cost: ");
        decimal waterCost = PromptForDecimal("Water cost");

        Console.Write("Heating cost: ");
        decimal heatingCost = PromptForDecimal("Heating cost");

        Console.Write("Gas cost: ");
        decimal gasCost = PromptForDecimal("Gas cost");

        Console.Write("Cleaning cost: ");
        decimal cleaningCost = PromptForDecimal("Cleaning cost");

        Console.Write("Management cost: ");
        decimal managementCost = PromptForDecimal("Management cost");

        Console.Write("Trash removal cost: ");
        decimal trashRemovalCost = PromptForDecimal("Trash removal cost");

        Console.Write("Internet, TV, and phone cost: ");
        decimal internetTvPhoneCost = PromptForDecimal("Internet, TV, and phone cost");

        UtilityExpenses expenses = new UtilityExpenses
        {
            ApartmentId = currentApartment.ApartmentNumber,
            ElectricityCost = electricityCost,
            WaterCost = waterCost,
            HeatingCost = heatingCost,
            GasCost = gasCost,
            CleaningCost = cleaningCost,
            ManagementCost = managementCost,
            TrashRemovalCost = trashRemovalCost,
            InternetTvPhoneCost = internetTvPhoneCost,
            UtilityExpensesMonth = inputDate
        };

        expenses.AllUtilityExpenses = electricityCost + waterCost + heatingCost + gasCost + cleaningCost + managementCost + trashRemovalCost + internetTvPhoneCost;

        currentApartment.UtilityExpenses.Add(expenses);

        Console.WriteLine("Utility expenses added successfully.");
    }
}