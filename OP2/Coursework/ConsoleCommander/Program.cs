using FuncCore.Buildings;
using FuncCore.DataBaseActions;
using FuncCore.DataBaseActions.Buildings;
using FuncCore.DataBaseActions.Persons;
using FuncCore.Persons;


namespace ConsoleCommander
{
    class Program
    {
        static void Main(string[] args)
        {
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
                        string buildingName = Console.ReadLine();

                        Console.WriteLine("Enter the number of floors:");
                        int numberOfFloors;
                        while (!int.TryParse(Console.ReadLine(), out numberOfFloors))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number for the number of floors:");
                        }

                        BuildingsContext.AddBuilding(buildingName, numberOfFloors);

                        break;
                    }


                    case "2":
                    {
                        Building? currentBuilding = null;
                        do
                        {
                            Console.WriteLine("Enter Building ID or Name (or type 'exit' to go back):");
                            var inputDatFind = Console.ReadLine()?.Trim();

                            if (inputDatFind?.ToLower() == "exit")
                            {
                                break;
                            }

                            if (long.TryParse(inputDatFind, out long id))
                            {
                                currentBuilding = BuildingsContext.FindBuildingById(new BuildingsContext(),id);
                            }
                            else
                            {
                                currentBuilding = BuildingsContext.FindBuildingByName(new BuildingsContext(),inputDatFind);
                            }

                            if (currentBuilding != null)
                            {
                                Console.WriteLine($"Found Building: ID = {currentBuilding.BuildingId}, Name = {currentBuilding.BuildingName}, Number of Floors = {currentBuilding.NumberOfFloors}");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Building not found. Try again or type 'exit' to go back.");
                            }
                        } while (true);

                        if (currentBuilding != null)
                        {
                            string action;
                            do
                            {
                                using var context = new ApartmentContext();
                                currentBuilding.Apartments = await context.FindApartmentsByBuildingId(currentBuilding.BuildingId);
                               
                                

                                
                                Console.WriteLine("\nSelect an action (or type 'exit' to go back):");
                                Console.WriteLine("1. Add apartment to the building");
                                Console.WriteLine("2. See all apartments in the building");
                                Console.WriteLine("3: Choose an apartment");
                                action = Console.ReadLine()?.ToLower();

                                switch (action)
                                {
                                    case "1":
                                    {
                                        Console.WriteLine(
                                            "\nEnter apartment details: number, room count, floor, cost per square meter.");
                                        var details = Console.ReadLine().Split(',');
                                        if (details.Length == 4 && int.TryParse(details[0], out int number) &&
                                            long.TryParse(details[1], out long roomCount) &&
                                            long.TryParse(details[2], out long floor) &&
                                            decimal.TryParse(details[3], out decimal costPerSquareMeter))
                                        {
                                            var existingApartment = ApartmentContext.GetApartmentByNumberAndBuildingId(
                                                new ApartmentContext(), number, currentBuilding.BuildingId);

                                            if (existingApartment != null)
                                            {
                                                Console.WriteLine(
                                                    $"\nError: Apartment with number {number} already exists in building.");
                                                break;
                                            }

                                            if (floor > currentBuilding.NumberOfFloors)
                                            {
                                                Console.WriteLine(
                                                    "Error: The floor of the apartment cannot exceed the maximum floor of the building.");
                                                break;
                                            }

                                            Console.WriteLine(
                                                $"\nConfirm adding apartment with details: Number = {number}, Room Count = {roomCount}, Floor = {floor} " +
                                                $"Cost per square meter: {costPerSquareMeter} (yes/no)?");
                                            var confirmation = Console.ReadLine()?.ToLower();
                                            if (confirmation == "yes")
                                            {
                                                ApartmentContext.AddApartment(new ApartmentContext(),number, roomCount,
                                                    floor, currentBuilding.BuildingId, costPerSquareMeter,
                                                    currentBuilding);
                                                Console.WriteLine("\nApartment added successfully!");
                                            }
                                            else
                                            {
                                                Console.WriteLine("\nApartment addition canceled.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine(
                                                "\nInvalid input. Make sure to enter the details in the format: number,room count,floor.");
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
                                                    $"Apartment ID: {apartment.ApartmetId}, Number: {apartment.ApartmentNumber}, Room Count: {apartment.RoomCount}, Floor: {apartment.Floor}" +
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
                                            var currentApartment =
                                                ApartmentContext.GetApartmentByNumberAndBuildingId(
                                                    new ApartmentContext(), apartmentNumber,
                                                    currentBuilding.BuildingId);

                                            if (currentApartment != null)
                                            {
                                                do
                                                {
                                                    var possibleResidents =
                                                        PersonContext.FindResidentsByApartmentId(new PersonContext(),
                                                            currentApartment.ApartmetId);
                                                    currentApartment.Residents =
                                                        possibleResidents ?? new List<Person>();

                                                    Console.WriteLine(
                                                        $"Apartment found with number {apartmentNumber}. What would you like to do?");
                                                    Console.WriteLine("1. View details");
                                                    Console.WriteLine("2. Edit data about residents");
                                                    Console.WriteLine("3. Edit cost per square meter");
                                                    Console.WriteLine("4. Delete apartment");
                                                    Console.WriteLine("5. ");
                                                    Console.WriteLine("6. Go back\n\n");
                                                    
                                                    Console.WriteLine("Choose an action:");
                                                    Console.WriteLine("1. Update apartment information");
                                                    Console.WriteLine("2. Register new residents");
                                                    Console.WriteLine("3. Update resident information");
                                                    Console.WriteLine("4. Remove a resident");
                                                    Console.WriteLine("5. Calculate apartment charges");
                                                    Console.WriteLine("6. View apartment information");
                                                    Console.WriteLine("7. Exit");

                                                    var actionChoice = Console.ReadLine();


                                                    switch (actionChoice)
                                                    {

                                                        case "1":
                                                        {
                                                            
                                                            
                                                            break;
                                                        }

                                                        case "2":
                                                        {
                                                            Console.Write("Enter resident's name: ");
                                                            string residentName =
                                                                (Console.ReadLine() ?? string.Empty).Trim();
                                                            if (string.IsNullOrWhiteSpace(residentName))
                                                            {
                                                                Console.WriteLine(
                                                                    "Resident's name cannot be empty. Try again.");
                                                            }

                                                            PersonContext.AddResident(new PersonContext(),
                                                                residentName, currentApartment.ApartmetId);

                                                            break;
                                                        }


                                                        case "3":
                                                        {
                                                            Console.WriteLine("Enter new cost per square meter:");
                                                            var newCostInput = Console.ReadLine();
                                                            if (decimal.TryParse(newCostInput, out decimal newCost))
                                                            {
                                                                // ApartmentContext.UpdateCostPerSquareMeter(
                                                                //     currentApartment, newCost);
                                                                // Console.WriteLine(
                                                                //     "Cost per square meter updated successfully.");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine(
                                                                    "Invalid input for cost per square meter.");
                                                            }
                                                            break;
                                                        }
                                                            
                                                        case "6":
                                                            currentApartment.PrintApartmentDetails();
                                                            break;
                                                            
                                                            

                                                            break;
                                                        case "123":
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


                                    case "exit":
                                        break;
                                    default:
                                        Console.WriteLine("\nInvalid action. Please try again.");
                                        break;
                                }
                            } while (action != "exit");
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
                        Console.WriteLine("Невідома команда, спробуйте ще раз.");
                        break;
                }
            } while (working);
        }
    }
}