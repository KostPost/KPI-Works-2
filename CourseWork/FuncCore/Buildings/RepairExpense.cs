namespace FuncCore;

public class RepairExpense
{
    public double Cost { get; set; }
    public string Description { get; set; }


    public static void AddRepairExpense(Apartment apartment)
    {
        try
        {
            Console.WriteLine("Enter the repair expense description:");
            string description = Console.ReadLine();
            Console.WriteLine("Enter the repair expense cost:");
            if (double.TryParse(Console.ReadLine(), out double cost))
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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static void CloseRepairExpense(Apartment apartment)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static void CheckAllRepairExpenses(Building building)
    {
        try
        {
            Console.WriteLine("Please enter the landlord's name:");
            string landlordName = Console.ReadLine();

            var landLord = building.LandLords.FirstOrDefault(ll =>
                ll.FullName.Equals(landlordName, StringComparison.OrdinalIgnoreCase));

            double totalRepairExpenses = 0.0;

            foreach (var apartment in landLord.OwnedApartments)
            {
                double apartmentRepairExpenses = apartment.RepairExpenses.Sum(repairExpense => repairExpense.Cost);

                totalRepairExpenses += apartmentRepairExpenses;

                Console.WriteLine(
                    $"Repair expenses for apartment with number: {apartment.ApartmentNumber} is : {apartmentRepairExpenses}");
            }

            Console.WriteLine($"Total repair expenses for landlord {landlordName} is : {totalRepairExpenses}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
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