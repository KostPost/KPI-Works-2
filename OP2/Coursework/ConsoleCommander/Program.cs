using FuncCore.Buildings;
using FuncCore.DataBaseActions;


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
                        BuildingsContext.AddBuilding();

                        break;
                    }

                    case "2":
                    {
                        Building currentBuilding = null;

                        do
                        {
                            Console.WriteLine("Enter Building ID or Name (or type 'exit' to go back):");
                            var inputDatFind = Console.ReadLine();

                            if (inputDatFind?.ToLower() == "exit")
                            {
                                break; // Выход из внешнего цикла
                            }

                            if (long.TryParse(inputDatFind, out long id))
                            {
                                currentBuilding = BuildingsContext.FindBuildingById(new BuildingsContext(), id);
                            }
                            else
                            {
                                currentBuilding =
                                    BuildingsContext.FindBuildingByName(new BuildingsContext(), inputDatFind);
                            }

                            if (currentBuilding != null)
                            {
                                Console.WriteLine(
                                    $"Found Building: ID = {currentBuilding.BuildingId}, Name = {currentBuilding.BuildingName}, Number of Floors = {currentBuilding.NumberOfFloors}");
                               
                                
                                currentBuilding.Apartments = ApartmentContext.FindApartmentsByBuildingId(
                                    new ApartmentContext(),
                                    currentBuilding.BuildingId);

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
                                action = Console.ReadLine()?.ToLower();

                                switch (action)
                                {
                                    case "1":
                                    {
                                        Console.WriteLine("\nEnter apartment details: number, room count, floor, cost per square meter.");
                                        var details = Console.ReadLine().Split(',');
                                        if (details.Length == 4 && int.TryParse(details[0], out int number) &&
                                            long.TryParse(details[1], out long roomCount) &&
                                            long.TryParse(details[2], out long floor) &&
                                            decimal.TryParse(details[3], out decimal costByOneM))
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
                                                $"\nConfirm adding apartment with details: Number = {number}, Room Count = {roomCount}, Floor = {floor} (yes/no)?");
                                            var confirmation = Console.ReadLine()?.ToLower();
                                            if (confirmation == "yes")
                                            {
                                                ApartmentContext.AddApartment(new ApartmentContext(), number, roomCount,
                                                    floor, currentBuilding.BuildingId,costByOneM , currentBuilding);
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
                                                    $"Apartment ID: {apartment.ApartmetId}, Number: {apartment.Number}, Room Count: {apartment.RoomCount}, Floor: {apartment.Floor}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nNo apartments found in the building.");
                                        }

                                        break;


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