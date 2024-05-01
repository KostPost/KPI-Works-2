using FuncCore.Buildings;
using FuncCore.Persons;


namespace ConsoleCommander
{
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
                            string inputDatFind = Console.ReadLine()?.Trim();

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
                            string action;
                            do
                            {
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
                                                    // If user cancels, remove apartment from building
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
                                                    Console.WriteLine("7. Exit\n");

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
                                                                Console.WriteLine("Cost per square meter updated successfully.");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Invalid input for cost per square meter.");
                                                            }
                                                            break;
                                                        }


                                                        case "2":
                                                            Console.Write("Enter resident's name: ");
                                                            string residentName = Console.ReadLine()?.Trim();
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

                                                        case "4":
                                                        {
                                                            Console.WriteLine("List of current residents/tenants:");
                                                            currentApartment.PrintTenants(); 
                                                            Console.Write("Enter the full name of the resident/tenant to remove: ");
                                                            string residentNameToRemove = Console.ReadLine()?.Trim();
                                                            Tenant tenantToRemove = currentApartment.Tenants.FirstOrDefault(t =>
                                                                t.FullName.Equals(residentNameToRemove, StringComparison.OrdinalIgnoreCase));
                                                            if (tenantToRemove != null)
                                                            {
                                                                currentApartment.Tenants.Remove(tenantToRemove);
                                                                Console.WriteLine($"Resident/tenant '{residentNameToRemove}' removed from the apartment.");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine($"Resident/tenant '{residentNameToRemove}' not found in the apartment.");
                                                            }
                                                            break;
                                                        }

                                                        case "3":
                                                        {
                                                            Console.WriteLine("List of current residents/tenants:");
                                                            currentApartment.PrintTenants(); // Assuming you have a method to print tenants

                                                            Console.Write("Enter the full name of the resident/tenant to update: ");
                                                            string residentNameToUpdate = Console.ReadLine()?.Trim();

                                                            Tenant tenantToUpdate = currentApartment.Tenants.FirstOrDefault(t => t.FullName.Equals(residentNameToUpdate, StringComparison.OrdinalIgnoreCase));

                                                            if (tenantToUpdate != null)
                                                            {
                                                                Console.WriteLine($"Updating information for {residentNameToUpdate}:");
                                                                Console.Write("Enter new full name: ");
                                                                string newFullName = Console.ReadLine()?.Trim();
                                                                if (!string.IsNullOrWhiteSpace(newFullName))
                                                                {
                                                                    tenantToUpdate.FullName = newFullName;
                                                                    Console.WriteLine("Full name updated successfully.");
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("Invalid input for full name.");
                                                                }

                                                                Console.WriteLine($"Information for {residentNameToUpdate} updated successfully.");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine($"Resident/tenant '{residentNameToUpdate}' not found in the apartment.");
                                                            }

                                                            break;
                                                        }
                                                        
                                                        
                                                        case "5":
                                                        {
                                                            Console.Write("Enter landlord's name: ");
                                                            string landlordName = Console.ReadLine()?.Trim();
    
                                                            if (!string.IsNullOrWhiteSpace(landlordName))
                                                            {
                                                                LandLord newLandLord = new LandLord
                                                                {
                                                                    FullName = landlordName,
                                                                    ApartmentNumber = currentApartment.ApartmentNumber
                                                                };

                                                                currentApartment.ApartmentOwner = newLandLord;

                                                                Console.WriteLine($"Landlord '{landlordName}' added to apartment {currentApartment.ApartmentNumber}.");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Landlord's name cannot be empty. Try again.");
                                                            }

                                                            break;
                                                        }

                                                        
                                                        case "6":
                                                            currentApartment.PrintApartmentDetails();
                                                            
                                                            Console.WriteLine("\n");
                                                            
                                                            currentApartment.PrintTenants();
                                                            break;
                                                        
                                                        case "7":
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