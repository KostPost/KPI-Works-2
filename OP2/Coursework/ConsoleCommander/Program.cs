using System.Globalization;
using FuncCore.Buildings;
using FuncCore.Persons;


namespace ConsoleCommander;

class Program
{
    static void Main(string[] args)
    {
        List<Building> buildings = new List<Building>();
        bool working = true;

        do
        {
            Console.WriteLine("\nElectronic directory of residents and tenants of a multi-apartment building\n");
            Console.WriteLine("Select an action:");
            Console.WriteLine("1: Create a building");
            Console.WriteLine("2: Select a building");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                {
                    Console.WriteLine("Enter the building name:");
                    string? buildingName = Console.ReadLine();

                    if (buildingName != null)
                    {
                        Console.WriteLine("Enter the number of floors:");
                        int numberOfFloors;
                        while (!int.TryParse(Console.ReadLine(), out numberOfFloors))
                        {
                            Console.WriteLine(
                                "Invalid input. Please enter a valid number for the number of floors:");
                        }

                        Building newBuilding = new Building(buildingName, numberOfFloors);

                        buildings.Add(newBuilding);
                    }
                    else
                    {
                        Console.WriteLine("Building name cannot be null.");
                    }

                    break;
                }

                case "2":
                {
                    Console.WriteLine(buildings.Count);

                    Building? currentBuilding = null;
                    do
                    {
                        if (buildings.Count == 0)
                        {
                            Console.WriteLine("No buildings available.");
                            break;
                        }

                        Console.WriteLine("Enter Name (or type 'exit' to go back):");
                        string? inputDatFind = Console.ReadLine()?.Trim();

                        if (inputDatFind?.ToLower() == "exit")
                        {
                            break;
                        }

                        currentBuilding = buildings.FirstOrDefault(building =>
                            building.BuildingName.ToLower() == inputDatFind.ToLower());


                        if (currentBuilding != null)
                        {
                            Console.WriteLine(
                                $"Found Building: Name = {currentBuilding.BuildingName}, Number of Floors = {currentBuilding.NumberOfFloors}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Building not found. Try again or type 'exit' to go back.");
                        }
                    } while (true);

                    if (currentBuilding != null)
                    {
                        string? action;
                        do
                        {
                            Console.WriteLine("\nSelect an action (or type 'exit' to go back):");
                            Console.WriteLine("1. Add apartment to the building");
                            Console.WriteLine("2. See all apartments in the building");
                            Console.WriteLine("3: Choose an apartment");
                            Console.WriteLine("4. ");
                            Console.WriteLine("5. Exit");
                            action = Console.ReadLine()?.ToLower();

                            switch (action)
                            {
                                case "1":
                                {
                                    Console.WriteLine(
                                        "\nEnter apartment details: number, room count, floor, cost per square meter.");
                                    var details = Console.ReadLine()?.Split(',');
                                    if (details?.Length == 4 && int.TryParse(details[0], out int number) &&
                                        long.TryParse(details[1], out long roomCount) &&
                                        long.TryParse(details[2], out long floor) &&
                                        decimal.TryParse(details[3], out decimal costPerSquareMeter))
                                    {
                                        Apartment newApartment = new Apartment(number, roomCount, floor,
                                            currentBuilding.BuildingId, costPerSquareMeter);

                                        if (currentBuilding.AddApartment(newApartment))
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
                                                currentBuilding.Apartments.Remove(newApartment);
                                                Console.WriteLine("\nApartment addition canceled.");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(
                                            "\nInvalid input. Make sure to enter the details in the format: number, room count, floor, cost per square meter.");
                                    }

                                    break;
                                }


                                case "2":
                                    var apartments = currentBuilding.Apartments;

                                    if (apartments.Any())
                                    {
                                        Console.WriteLine("\nApartments in the building:");
                                        foreach (var apartment in apartments)
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

                                    break;

                                case "3":
                                {
                                    bool workingWithApartment = true;

                                    Console.WriteLine("Enter the apartment number:");
                                    var apartmentNumberInput = Console.ReadLine();
                                    if (int.TryParse(apartmentNumberInput, out int apartmentNumber))
                                    {
                                        Apartment? currentApartment =
                                            Apartment.GetApartmentByNumber(apartmentNumber,
                                                currentBuilding.Apartments);


                                        if (currentApartment != null)
                                        {
                                            do
                                            {
                                                Console.WriteLine("Choose an action:");
                                                Console.WriteLine("1. Update apartment information");
                                                Console.WriteLine("2. Register new residents");
                                                Console.WriteLine("3. Update resident information");
                                                Console.WriteLine("4. Remove a resident");
                                                Console.WriteLine("5. Add a LandLord");
                                                Console.WriteLine("6. View apartment information");
                                                Console.WriteLine(
                                                    "7. Add utility costs and calculate rent for a specific month");
                                                Console.WriteLine("8. See information about UtilityExpenses");
                                                Console.WriteLine("9. Add a Room in Apartment");
                                                Console.WriteLine("10. Update Rent Date");
                                                Console.WriteLine("11 Add a RepaitExpense");
                                                Console.WriteLine("12. Exit\n");

                                                var actionChoice = Console.ReadLine();


                                                switch (actionChoice)
                                                {
                                                    case "1":
                                                    {
                                                        Console.WriteLine("Enter new cost per square meter:");
                                                        var newCostInput = Console.ReadLine();
                                                        if (decimal.TryParse(newCostInput, out decimal newCost))
                                                        {
                                                            currentApartment.CostPerSquareMeter = newCost;
                                                            Console.WriteLine(
                                                                "Cost per square meter updated successfully.");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(
                                                                "Invalid input for cost per square meter.");
                                                        }

                                                        break;
                                                    }

                                                    case "2":
                                                        Console.Write("Enter resident's name: ");
                                                        string? residentName = Console.ReadLine()?.Trim();
                                                        if (!string.IsNullOrWhiteSpace(residentName))
                                                        {
                                                            Tenant newTenant = new Tenant
                                                                { FullName = residentName };

                                                            currentApartment.Tenants.Add(newTenant);

                                                            Console.WriteLine(
                                                                $"Tenant '{residentName}' added to apartment {apartmentNumber}.");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(
                                                                "Resident's name cannot be empty. Try again.");
                                                        }

                                                        break;


                                                    case "3":
                                                    {
                                                        Console.WriteLine("List of current residents/tenants:");
                                                        currentApartment
                                                            .PrintTenants();

                                                        Console.Write(
                                                            "Enter the full name of the resident/tenant to update: ");
                                                        string? residentNameToUpdate = Console.ReadLine()?.Trim();

                                                        Tenant? tenantToUpdate =
                                                            currentApartment.Tenants.FirstOrDefault(t =>
                                                                t.FullName.Equals(residentNameToUpdate,
                                                                    StringComparison.OrdinalIgnoreCase));

                                                        if (tenantToUpdate != null)
                                                        {
                                                            Console.WriteLine(
                                                                $"Updating information for {residentNameToUpdate}:");
                                                            Console.Write("Enter new full name: ");
                                                            string? newFullName = Console.ReadLine()?.Trim();
                                                            if (!string.IsNullOrWhiteSpace(newFullName))
                                                            {
                                                                tenantToUpdate.FullName = newFullName;
                                                                Console.WriteLine(
                                                                    "Full name updated successfully.");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Invalid input for full name.");
                                                            }

                                                            Console.WriteLine(
                                                                $"Information for {residentNameToUpdate} updated successfully.");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(
                                                                $"Resident/tenant '{residentNameToUpdate}' not found in the apartment.");
                                                        }

                                                        break;
                                                    }

                                                    case "4":
                                                    {
                                                        Console.WriteLine("List of current residents/tenants:");
                                                        currentApartment.PrintTenants();
                                                        Console.Write(
                                                            "Enter the full name of the resident/tenant to remove: ");
                                                        string? residentNameToRemove = Console.ReadLine()?.Trim();

                                                        Tenant? tenantToRemove =
                                                            currentApartment.Tenants.FirstOrDefault(t =>
                                                                t.FullName.Equals(residentNameToRemove,
                                                                    StringComparison.OrdinalIgnoreCase));

                                                        if (tenantToRemove != null)
                                                        {
                                                            currentApartment.Tenants.Remove(tenantToRemove);

                                                            if (currentApartment.Tenants.Count == 0)
                                                                currentApartment.IsOccupied = false;

                                                            Console.WriteLine(
                                                                $"Resident/tenant '{residentNameToRemove}' removed from the apartment.");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(
                                                                $"Resident/tenant '{residentNameToRemove}' not found in the apartment.");
                                                        }

                                                        break;
                                                    }


                                                    case "5":
                                                    {
                                                        Console.Write("Enter landlord's name: ");
                                                        string? landlordName = Console.ReadLine()?.Trim();

                                                        if (!string.IsNullOrWhiteSpace(landlordName))
                                                        {
                                                            LandLord newLandLord = new LandLord
                                                            {
                                                                FullName = landlordName,
                                                                ApartmentNumber = currentApartment.ApartmentNumber
                                                            };

                                                            currentApartment.ApartmentOwner = newLandLord;

                                                            Console.WriteLine(
                                                                $"Landlord '{landlordName}' added to apartment {currentApartment.ApartmentNumber}.");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(
                                                                "Landlord's name cannot be empty. Try again.");
                                                        }

                                                        break;
                                                    }


                                                    case "6":
                                                        currentApartment.PrintApartmentDetails();

                                                        Console.WriteLine("\n");

                                                        currentApartment.PrintTenants();
                                                        break;

                                                    case "7":
                                                    {
                                                        if (!currentApartment.RentTermStart.Equals(DateTime.MinValue) &&
                                                            !currentApartment.RentTermEnd.Equals(DateTime.MinValue))
                                                        {
                                                            Console.WriteLine(
                                                                $"Current rent term: {currentApartment.RentTermStart:d} - {currentApartment.RentTermEnd:d}");
                                                            UtilityExpenses
                                                                .AddUtilityExpensesForMonth(currentApartment);
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(
                                                                "Rent term not set for the current apartment.");
                                                        }

                                                        break;
                                                    }

                                                    case "8":
                                                    {
                                                        if (!currentApartment.RentTermStart.Equals(DateTime.MinValue) &&
                                                            !currentApartment.RentTermEnd.Equals(DateTime.MinValue))
                                                        {
                                                            Console.WriteLine(
                                                                $"Current rent term: {currentApartment.RentTermStart:d} - {currentApartment.RentTermEnd:d}");
                                                            UtilityExpenses.ViewUtilityExpensesForMonth(
                                                                currentApartment);
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(
                                                                "Rent term not set for the current apartment.");
                                                        }

                                                        break;
                                                    }

                                                    case "9":
                                                    {
                                                        Console.WriteLine("Enter room details:");
                                                        Console.WriteLine("Room number:");
                                                        int roomNumber;
                                                        while (!int.TryParse(Console.ReadLine(), out roomNumber))
                                                        {
                                                            Console.WriteLine(
                                                                "Invalid input. Please enter a valid room number:");
                                                        }

                                                        Console.WriteLine("Area:");
                                                        double area;
                                                        while (!double.TryParse(Console.ReadLine(), out area))
                                                        {
                                                            Console.WriteLine(
                                                                "Invalid input. Please enter a valid area:");
                                                        }

                                                        currentApartment.AddRoom(roomNumber, area);

                                                        Console.WriteLine(
                                                            "Room added successfully to the apartment.");
                                                        break;
                                                    }

                                                    case "10":
                                                    {
                                                        Console.Write(
                                                            "Enter the new rent start date (yyyy-MM-dd): ");
                                                        if (!DateTime.TryParseExact(Console.ReadLine(),
                                                                "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                                                DateTimeStyles.None, out DateTime newRentStartDate))
                                                        {
                                                            Console.WriteLine(
                                                                "Invalid date format. Please enter the date in yyyy-MM-dd format.");
                                                            break;
                                                        }

                                                        Console.Write("Enter the new rent end date (yyyy-MM-dd): ");
                                                        if (!DateTime.TryParseExact(Console.ReadLine(),
                                                                "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                                                DateTimeStyles.None, out DateTime newRentEndDate))
                                                        {
                                                            Console.WriteLine(
                                                                "Invalid date format. Please enter the date in yyyy-MM-dd format.");
                                                            break;
                                                        }

                                                        currentApartment.RentTermStart = newRentStartDate;
                                                        currentApartment.RentTermEnd = newRentEndDate;

                                                        Console.WriteLine(
                                                            "Rent start and end dates updated successfully.");
                                                        break;
                                                    }

                                                    case "11":
                                                    {
                                                        Console.Write("Enter the cost of repair: ");
                                                        decimal repairCost;
                                                        while (!decimal.TryParse(Console.ReadLine(), out repairCost) ||
                                                               repairCost < 0)
                                                        {
                                                            Console.WriteLine(
                                                                "Invalid input. Please enter a valid repair cost:");
                                                        }

                                                        Console.Write("Enter the description of repair: ");
                                                        string repairDescription = Console.ReadLine();

                                                        currentApartment.AddRepairExpense(repairCost, repairDescription);
                                                        Console.WriteLine("Repair expenses added successfully.");


                                                        Console.WriteLine(
                                                            $"Apartment with number {apartmentNumber} not found.");

                                                        break;
                                                    }


                                                    case "12":
                                                        workingWithApartment = false;

                                                        break;
                                                    default:
                                                        Console.WriteLine("Invalid choice.");
                                                        break;
                                                }
                                            } while (workingWithApartment);
                                        }
                                        else
                                        {
                                            Console.WriteLine(
                                                $"Apartment with number {apartmentNumber} not found in this building.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(
                                            "Invalid input. Please enter a valid apartment number.");
                                    }


                                    break;
                                }

                                case "4":
                                {
                                    Console.Write("Enter the landlord's name: ");
                                    string landlordName = Console.ReadLine()!;

                                    List<Apartment> landLordApartmetns =
                                        LandLord.FindLandLordApartments(landlordName, currentBuilding);

                                    Console.WriteLine("\n\n" + landLordApartmetns.Count + "\n\n");

                                    if (landLordApartmetns.Count != 0)
                                    {
                                        decimal totalIncome = LandLord.CalculateRentIncome(currentBuilding.Apartments);

                                        Console.WriteLine($"Total rent income for {landlordName}: {totalIncome:C}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Landlord '{landlordName}' not found.");
                                    }

                                    break;
                                }


                                case "5":
                                    break;
                                default:
                                    Console.WriteLine("\nInvalid action. Please try again.");
                                    break;
                            }
                        } while (action != "4");
                    }

                    break;
                }


                case "4":
                {
                    Console.WriteLine("Exiting...");
                    working = false;
                    break;
                }

                default:
                    Console.WriteLine("");
                    break;
            }
        } while (working);
    }
}