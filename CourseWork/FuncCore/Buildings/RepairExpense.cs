namespace FuncCore;

public class RepairExpense
{
    public decimal Cost { get; set; }
    public string Description { get; set; }
    
    
    public static void AddRepairExpense(Apartment apartment)
    {
        Console.WriteLine("Enter the repair expense description:");
        string description = Console.ReadLine();
        Console.WriteLine("Enter the repair expense cost:");
        if (decimal.TryParse(Console.ReadLine(), out decimal cost))
        {
            var repairExpense = new RepairExpense { Description = description, Cost = cost };
            apartment.RepairExpenses.Add(repairExpense);
            Console.WriteLine("Repair expense added successfully.");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid decimal number.");
        }
    }

    public static void CloseRepairExpense(Apartment apartment)
    {
        Console.WriteLine("Enter the description of the repair expense you want to close:");
        string description = Console.ReadLine();
        var expense = apartment.RepairExpenses.FirstOrDefault(e => e.Description == description);
        if (expense != null)
        {
            apartment.RepairExpenses.Remove(expense);
            Console.WriteLine($"Repair expense '{description}' has been closed.");
        }
        else
        {
            Console.WriteLine($"No repair expense found with description '{description}'.");
        }
    }

    public static void PrintRepairExpenses(List<RepairExpense> repairExpenses)
    {
        if (repairExpenses.Any())
        {
            foreach (var repairExpense in repairExpenses)
            {
                Console.WriteLine($"Description: {repairExpense.Description}");
                Console.WriteLine($"Cost: {repairExpense.Cost:C}");
                Console.WriteLine("");
            }
        
            var totalRepairExpenses = repairExpenses.Sum(expense => expense.Cost);
            Console.WriteLine($"Total Repair Expenses: {totalRepairExpenses:C}");
        }
        else
        {
            Console.WriteLine("No repair expenses data available.");
        }
    }
}