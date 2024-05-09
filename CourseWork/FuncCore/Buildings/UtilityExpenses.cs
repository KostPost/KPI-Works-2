using System.Globalization;

namespace FuncCore;

public class UtilityExpense
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

    public static void AddUtilityCosts(Apartment apartment)
    {
        var expense = new UtilityExpense();
        
        Console.WriteLine("Enter the month for which to add utility costs (Format: MM-YYYY):");
        expense.UtilityExpensesMonth = DateTime.ParseExact(Console.ReadLine(), "MM-yyyy", CultureInfo.InvariantCulture);

        Console.WriteLine("Enter the rent cost:");
        expense.RentCost = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Enter the heating cost:");
        expense.HeatingCost = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Enter the water cost:");
        expense.WaterCost = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Enter the electricity cost:");
        expense.ElectricityCost = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Enter the gas cost:");
        expense.GasCost = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Enter the cleaning cost:");
        expense.CleaningCost = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Enter the management cost:");
        expense.ManagementCost = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Enter the trash removal cost:");
        expense.TrashRemovalCost = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Enter the internet, TV, and phone cost:");
        expense.InternetTvPhoneCost = decimal.Parse(Console.ReadLine());

        expense.AllUtilityExpenses = expense.HeatingCost + expense.WaterCost + expense.ElectricityCost + expense.GasCost + 
                                     expense.CleaningCost + expense.ManagementCost + expense.TrashRemovalCost + expense.InternetTvPhoneCost;

        Console.WriteLine("Utility expenses added successfully for month: " + expense.UtilityExpensesMonth.ToString("MM-yyyy"));

        
        apartment.UtilityExpenses.Add(expense);
    }
    public static void PrintUtilityExpenses(List<UtilityExpense> utilityExpenses)    
    {
        if (utilityExpenses?.Any() == true)
        {
            foreach (var expense in utilityExpenses)
            {
                Console.WriteLine($"Apartment ID: {expense.ApartmentId}");
                Console.WriteLine($"Utility Expenses Month: {expense.UtilityExpensesMonth}");
                Console.WriteLine($"Rent Cost: {expense.RentCost:C}");
                Console.WriteLine($"All Utility Expenses: {expense.AllUtilityExpenses:C}");
                Console.WriteLine($"Heating Cost: {expense.HeatingCost:C}");
                Console.WriteLine($"Water Cost: {expense.WaterCost:C}");
                Console.WriteLine($"Electricity Cost: {expense.ElectricityCost:C}");
                Console.WriteLine($"Gas Cost: {expense.GasCost:C}");
                Console.WriteLine($"Cleaning Cost: {expense.CleaningCost:C}");
                Console.WriteLine($"Management Cost: {expense.ManagementCost:C}");
                Console.WriteLine($"Trash Removal Cost: {expense.TrashRemovalCost:C}");
                Console.WriteLine($"Internet, TV, and Phone Cost: {expense.InternetTvPhoneCost:C}");
                Console.WriteLine("");
            }
        
            var totalRentCost = utilityExpenses.Sum(item => item.RentCost);
            Console.WriteLine($"Total Rent Cost: {totalRentCost:C}");
        }
        else
        {
            Console.WriteLine("No utility expenses data available.");
        }
    }
    
}