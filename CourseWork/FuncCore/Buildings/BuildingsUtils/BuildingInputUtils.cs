namespace FuncCore;

public class BuildingInputUtils
{
    public long NumberOfFloors { get; set; }

    public BuildingInputUtils(long numberOfFloors)
    {
        NumberOfFloors = numberOfFloors;
    }

    public int PromptForInt(string message)
    {
        int value;
        do
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if (int.TryParse(input, out value))
                return value;
            Console.WriteLine("Invalid input. Please enter a valid number.");
        } while (true);
    }

    public long PromptForLong(string message)
    {
        long value;
        do
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if (long.TryParse(input, out value))
                return value;
            Console.WriteLine("Invalid input. Please enter a valid number.");
        } while (true);
    }

    public int PromptForFloor(string message)
    {
        int floor;
        do
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if (int.TryParse(input, out floor) && floor <= NumberOfFloors)
                return floor;
            Console.WriteLine($"Invalid Floor. The building has only {NumberOfFloors} floors.");
        } while (true);
    }

    public double PromptForDouble(string message)
    {
        double value;
        do
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if (double.TryParse(input, out value))
                return value;
            Console.WriteLine("Invalid input. Please enter a valid number.");
        } while (true);
    }
    public void ChangeApartmentDetail(ref Apartment apartment)
    {
        while (true)
        {
            Console.WriteLine("Which detail do you want to change?");
            Console.WriteLine("1: Apartment Number\n2: Room Count\n3: Floor\n4: Cost Per Square Meter");

            switch (Console.ReadLine())
            {
                case "1":
                    apartment.ApartmentNumber = PromptForInt("New Apartment Number: ");
                    return;
                case "2":
                    apartment.RoomCount = PromptForLong("New Room Count: ");
                    return;
                case "3":
                    apartment.Floor = PromptForFloor("New Floor: ");
                    return;
                case "4":
                    apartment.CostPerSquareMeter = PromptForDouble("New Cost Per Square Meter: ");
                    return;
                default:
                    Console.WriteLine("Invalid option selected. Please select among the options (1-4).");
                    break;
            }
        }
    }
}