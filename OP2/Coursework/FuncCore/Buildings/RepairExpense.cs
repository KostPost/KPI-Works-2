namespace FuncCore.Buildings;

public class RepairExpense
{
    public decimal Cost { get; set; }
    public string Description { get; set; }
    
    public static decimal CalculateAllRepairExpense(List<RepairExpense> repairExpenses)
    {
        decimal totalCost = 0;
        if (repairExpenses.Count >= 1)
        {
            foreach (RepairExpense repairExpense in repairExpenses)
            {
                totalCost += repairExpense.Cost;
            }
        }

        return totalCost;

    }
}