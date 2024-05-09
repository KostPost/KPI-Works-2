namespace FuncCore;

public class RepairExpense
{
    public decimal Cost { get; set; }
    public string Description { get; set; }
    
    public static void PrintRepairExpenses(List<RepairExpense> repairExpenses)
    {
        if (repairExpenses?.Any() == true)
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