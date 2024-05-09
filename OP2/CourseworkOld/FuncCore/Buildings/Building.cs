using FuncCore.Persons;

namespace FuncCore.Buildings;

public class Building
{
    public long BuildingId;
    public string BuildingName { get; set; }

    public long NumberOfFloors { get; set; }

    public List<Apartment> Apartments { get; set; } = new List<Apartment>();

    public List<LandLord> LandLords { get; set; } = new List<LandLord>();

    private static long _lastBuildingId = 0;


    public Building(string buildingName, long numberOfFloors)
    {
        BuildingId = GenerateBuildingId();
        BuildingName = buildingName;
        NumberOfFloors = numberOfFloors;
    }

    public void FindAllLandlords()
    {

        foreach (var apartment in Apartments)
        {
            if (apartment.ApartmentOwner != null && !LandLords.Contains(apartment.ApartmentOwner))
            {
                LandLords.Add(apartment.ApartmentOwner);
            }
        }

        
    }
    private static long GenerateBuildingId()
    {
        return ++_lastBuildingId;
    }


    public static Building? FindBuildingByName(List<Building> buildings)
    {
        Console.WriteLine("Enter the building name:");
        string? buildingName = Console.ReadLine()?.Trim();

        if (!string.IsNullOrEmpty(buildingName))
        {
            return buildings.FirstOrDefault(building =>
                building.BuildingName.Equals(buildingName, StringComparison.OrdinalIgnoreCase));
        }
        else
        {
            Console.WriteLine("Invalid building name input.");
            return null;
        }
    }

    public static Building? CreateBuildingFromUserInput()
    {
        Console.WriteLine("Enter the building name:");
        string? buildingName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(buildingName))
        {
            Console.WriteLine("Building name cannot be empty.");
            return null;
        }

        Console.WriteLine("Enter the number of floors:");
        if (!int.TryParse(Console.ReadLine(), out int numberOfFloors) || numberOfFloors <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid number for the number of floors.");
            return null;
        }

        return new Building(buildingName, numberOfFloors);
    }

    public static void PrintAllBuildings(List<Building> buildings)
    {
        if (buildings.Count == 0)
        {
            Console.WriteLine("No buildings available.");
        }
        else
        {
            Console.WriteLine("\nList of Buildings:");
            foreach (var building in buildings)
            {
                Console.WriteLine($"Name: {building.BuildingName}, Number of Floors: {building.NumberOfFloors}");
            }
        }
    }

    public void DisplayApartments()
    {
        if (Apartments.Any())
        {
            Console.WriteLine("\nApartments in the building:");
            foreach (var apartment in Apartments)
            {
                Console.WriteLine(
                    $"Number: {apartment.ApartmentNumber}, Room Count: {apartment.RoomCount}, Floor: {apartment.Floor}" +
                    $", Cost per square meter: {apartment.CostPerSquareMeter}");
            }
        }
        else
        {
            Console.WriteLine("\nNo apartments found in the building.");
        }
    }

    public void AddNewApartment()
    {
        Console.WriteLine("\nEnter apartment details: number, room count, floor, cost per square meter.");
        var details = Console.ReadLine()?.Split(',');
        if (details?.Length == 4 && int.TryParse(details[0], out int number) &&
            long.TryParse(details[1], out long roomCount) &&
            long.TryParse(details[2], out long floor) &&
            decimal.TryParse(details[3], out decimal costPerSquareMeter))
        {
            Apartment newApartment = new Apartment(number, roomCount, floor,
                BuildingId, costPerSquareMeter);

            if (AddApartment(newApartment))
            {
                Console.WriteLine(
                    $"\nConfirm adding apartment with details: Number = {number}, Room Count = {roomCount}, Floor = {floor}, " +
                    $"Cost per square meter: {costPerSquareMeter} (yes/no)?");
                var confirmation = Console.ReadLine()?.ToLower();
                if (confirmation == "yes")
                {
                    Console.WriteLine("\nApartment added successfully!");
                }
                else
                {
                    this.Apartments.Remove(newApartment);
                    Console.WriteLine("\nApartment addition canceled.");
                }
            }
        }
        else
        {
            Console.WriteLine(
                "\nInvalid input. Make sure to enter the details in the format: number, room count, floor, cost per square meter.");
        }
    }


    private bool AddApartment(Apartment newApartment)
    {
        try
        {
            if (newApartment.ApartmentNumber <= 0)
            {
                throw new ArgumentException("Apartment number must be a positive integer.");
            }

            if (Apartments.Any(apartment => apartment.ApartmentNumber == newApartment.ApartmentNumber))
            {
                throw new InvalidOperationException(
                    $"Apartment with number {newApartment.ApartmentNumber} already exists in building {BuildingName}.");
            }

            if (newApartment.Floor > NumberOfFloors)
            {
                throw new InvalidOperationException(
                    "The floor of the apartment cannot exceed the maximum floor of the building.");
            }

            Apartments.Add(newApartment);
            return true;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }
}