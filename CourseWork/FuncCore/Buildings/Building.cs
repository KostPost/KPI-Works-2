using FuncCore.Persons;

namespace FuncCore;

public class Building
{
    public string BuildingName { get; set; }

    public long NumberOfFloors { get; set; }

    public List<Apartment> Apartments { get; set; }

    public List<LandLord> LandLords { get; set; }
    
    private BuildingInputUtils _inputUtils;

    private Building()
    {
        Apartments = new List<Apartment>();
        LandLords = new List<LandLord>();
    }

    public void CheckAllRepairExpenses()
    {
        Console.WriteLine("Please enter the landlord's name:");
        string landlordName = Console.ReadLine();

        var landLord = LandLords.FirstOrDefault(ll => ll.FullName.Equals(landlordName, StringComparison.OrdinalIgnoreCase));
    
        double totalRepairExpenses = 0.0;

        foreach (var apartment in landLord.OwnedApartments)
        {
            double apartmentRepairExpenses = apartment.RepairExpenses.Sum(repairExpense => repairExpense.Cost);

            totalRepairExpenses += apartmentRepairExpenses;

            Console.WriteLine($"Repair expenses for apartment with number: {apartment.ApartmentNumber} is : {apartmentRepairExpenses}");
        }

        Console.WriteLine($"Total repair expenses for landlord {landlordName} is : {totalRepairExpenses}");
    }
    public void CalculateLandlordIncome()
    {
        Console.WriteLine("Enter Landlord's name:");
        string landlordName = Console.ReadLine()?.Trim();

        var landLord = LandLords.FirstOrDefault(ll => ll.FullName.Equals(landlordName, StringComparison.OrdinalIgnoreCase));

        if (landLord == null)
        {
            Console.WriteLine($"No landlord found with the name '{landlordName}'.");
            return;
        }

        Console.WriteLine("Enter the start date for rent calculation (format: yyyy-mm-dd):");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime globalStartDate))
        {
            Console.WriteLine("Invalid start date entered. Try again.");
            return;
        }

        Console.WriteLine("Enter the end date for rent calculation (format: yyyy-mm-dd):");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime globalEndDate))
        {
            Console.WriteLine("Invalid end date entered. Try again.");
            return;
        }

        if (globalEndDate < globalStartDate)
        {
            Console.WriteLine("End date must be after start date.");
            return;
        }

        double totalIncome = 0;

        foreach (var apartment in landLord.OwnedApartments)
        {
            DateTime apartmentStartDate = (apartment.RentTermStart > globalStartDate) ? apartment.RentTermStart : globalStartDate;
            DateTime apartmentEndDate = (apartment.RentTermEnd < globalEndDate) ? apartment.RentTermEnd : globalEndDate;

            if (apartmentStartDate > apartmentEndDate)
            {
                continue;
            }

            int months = 0;
            while (apartmentStartDate.AddMonths(months) < apartmentEndDate)
            {
                months++;
            }

            double apartmentArea = apartment.Rooms.Sum(room => room.Area);
            totalIncome += apartmentArea * apartment.CostPerSquareMeter * months;
        }

        Console.WriteLine($"Total income from {landlordName}'s apartments between {globalStartDate.ToShortDateString()} and {globalEndDate.ToShortDateString()} is: {totalIncome}");
    }
    public void FindAllInfoAboutLandLord()
    {
        Console.WriteLine("Enter Landlord's name:");
        string landlordName = Console.ReadLine();

        var landLord = LandLords.FirstOrDefault(ll => ll.FullName.Equals(landlordName, StringComparison.OrdinalIgnoreCase));

        if (landLord == null)
        {
            Console.WriteLine($"No landlord found with the name '{landlordName}'.");
        }
        else
        {
            Console.WriteLine($"Landlord Name: {landLord.FullName}, Owned Apartments: {landLord.OwnedApartments.Count}");

            foreach (var apartment in landLord.OwnedApartments)
            {
                Console.WriteLine($"Apartment Info:");
                apartment.PrintApartmentDetails();
            }
        }
    }
    public void PrintAllLandLords()
    {
        if(LandLords.Count > 0)
        {
            foreach(var landLord in LandLords)
            {
                Console.WriteLine($"LandLord Name: {landLord.FullName}, Owned Apartments: {landLord.OwnedApartments.Count}");
            }
        }
        else
        {
            Console.WriteLine("No landlords present in the building.");
        }
    }
    
    public void AddApartment()
    {
        Apartment newApartment = new Apartment();
        bool continueInput = true;

        while (continueInput)
        {
            newApartment.ApartmentNumber = _inputUtils.PromptForInt("Apartment Number: ");
            if (Apartments.Any(a => a.ApartmentNumber == newApartment.ApartmentNumber))
            {
                Console.WriteLine("An apartment with this number already exists. Please provide a different number.");
                continue;
            }
            
            newApartment.RoomCount = _inputUtils.PromptForLong("Room Count: ");
            newApartment.Floor = _inputUtils.PromptForFloor("Floor: ");
            newApartment.CostPerSquareMeter = _inputUtils.PromptForDouble("Cost Per Square Meter: ");

           

            string? response = null;
            do
            {
                Console.WriteLine("\nApartment details:");
                Console.WriteLine($"Apartment Number: {newApartment.ApartmentNumber}");
                Console.WriteLine($"Room Count: {newApartment.RoomCount}");
                Console.WriteLine($"Floor: {newApartment.Floor}");
                Console.WriteLine($"Cost Per Square Meter: {newApartment.CostPerSquareMeter}\n");
                Console.WriteLine("Apartment data input successful.\nDo you want to save it? (yes/no/change)");
                response = Console.ReadLine()?.Trim().ToLower();

                switch (response)
                {
                    case "yes":
                        Apartments.Add(newApartment);
                        Console.WriteLine("Apartment added successfully.");
                        continueInput = false;
                        break;
                    case "no":
                        Console.WriteLine("Apartment not saved.\nDo you want to enter the data again? (yes/no)");
                        if (Console.ReadLine()?.Trim().ToLower() != "yes")
                            continueInput = false;
                        break;
                    case "change":
                        _inputUtils.ChangeApartmentDetail(ref newApartment);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please enter yes, no, or change.");
                        break;
                }
            } while (!(response == "yes" || response == "no"));        }
    }
    
    public Apartment GetApartmentByNumber()
    {
        Console.WriteLine("Enter the number of the apartment you want to select:");
        
        int apartmentNumber;
        while (true)
        {
            if(int.TryParse(Console.ReadLine(), out apartmentNumber))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid apartment number:");
            }
        }

        foreach (var apartment in this.Apartments)
        {
            if(apartment.ApartmentNumber == apartmentNumber)
            {
                return apartment;
            }
        }

        Console.WriteLine($"No apartment found with the number: {apartmentNumber}");
        return null;
    }
    
    public static void CreateBuilding(List<Building> buildings)
    {
        try
        {
            Console.Write("Enter the building name: ");
            string buildingName = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(buildingName))
            {
                Console.WriteLine("Building name cannot be empty.");
                return;
            }

            Console.Write("Enter the number of floors: ");
            if (!long.TryParse(Console.ReadLine(), out long numberOfFloors) || numberOfFloors <= 0)
            {
                Console.WriteLine("Invalid number of floors.");
                return;
            }

            long buildingId = buildings.Count + 1;

            Building newBuilding = new Building
            {
                BuildingName = buildingName,
                NumberOfFloors = numberOfFloors
            };

            newBuilding._inputUtils = new BuildingInputUtils(numberOfFloors);

            buildings.Add(newBuilding);
           
            Console.WriteLine($"Building '{buildingName}' with ID {buildingId} created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static Building? SelectBuilding(List<Building> buildings)
    {
        if (buildings.Count == 0)
        {
            Console.WriteLine("There are no buildings to select.");
        }
        else
        {
            Console.WriteLine("Select a building:");
            for (int i = 0; i < buildings.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {buildings[i].BuildingName}");
            }

            var buildingIndexInput = Console.ReadLine();
            if (int.TryParse(buildingIndexInput, out int buildingIndex) && buildingIndex >= 1 &&
                buildingIndex <= buildings.Count)
            {
                var selectedBuilding = buildings[buildingIndex - 1];
                Console.WriteLine($"You selected building '{selectedBuilding.BuildingName}");
                return selectedBuilding;
            }
        }

        return null;
    }

    public static void PrintBuildings(List<Building> buildings)
    {
        if (buildings.Count == 0)
        {
            Console.WriteLine("No buildings to print.");
        }
        else
        {
            Console.WriteLine("List of Buildings:");
            foreach (var building in buildings)
            {
                Console.WriteLine($"Name: {building.BuildingName}, Number of Floors: {building.NumberOfFloors}");
            }
        }
    }
}