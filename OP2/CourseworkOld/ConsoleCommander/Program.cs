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
            Console.WriteLine("3. Print all Buildings");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                {
                    Building? newBuilding = Building.CreateBuildingFromUserInput();
                    if (newBuilding != null)
                    {
                        buildings.Add(newBuilding);
                    }

                    break;
                }

                case "2":
                {
                    Building? currentBuilding = null;

                    do
                    {
                        currentBuilding = Building.FindBuildingByName(buildings);

                    } while (currentBuilding == null);
                    
                    if (currentBuilding != null)
                    {
                        string? action;
                        do
                        {
                            Console.WriteLine("\nSelect an action (or type 'exit' to go back):");
                            Console.WriteLine("1. Add apartment to the building");
                            Console.WriteLine("2. See all apartments in the building");
                            Console.WriteLine("3. Choose an apartment");
                            Console.WriteLine("4. Actions with LandLords");
                            Console.WriteLine("5. Exit");
                            action = Console.ReadLine()?.ToLower();

                            switch (action)
                            {
                                case "1":
                                {
                                    currentBuilding.AddNewApartment();
                                    break;
                                }

                                case "2":
                                {
                                    currentBuilding.DisplayApartments();
                                    break;
                                }

                                case "3":
                                {
                                    bool workingWithApartment = true;
                                    Apartment? currentApartment =  Apartment.GetApartmentByNumber(currentBuilding.Apartments);;
                                        
                                        
                                        if (currentApartment != null)
                                        {
                                            do
                                            {
                                                Console.WriteLine("\nChoose an action:");
                                                Console.WriteLine("1. Apartment Management\n" +
                                                                  "2. Resident Management\n" +
                                                                  "3. Landlord Management\n" +
                                                                  "4. Financial Transactions\n" +
                                                                  "5. Exit");

                                                var actionChoice = Console.ReadLine();
                                                
                                                switch (actionChoice)
                                                {

                                                    case "1":
                                                    {
                                                        Console.WriteLine("Choose an action - Apartment Management ");
                                                        Console.WriteLine("1. Update cost per information\n" +
                                                                          "2. Add a Room in Apartment\n" +
                                                                          "3. View apartment information\n");
                                                        actionChoice = Console.ReadLine();

                                                        switch (actionChoice)
                                                        {
                                                            case "1":
                                                            {
                                                                currentApartment.UpdateCostPerSquareMeter();
                                                                break;
                                                            }

                                                            case "2":
                                                            {
                                                                currentApartment.AddRoom();
                                                                break;
                                                            }


                                                            case "3":
                                                            {
                                                                currentApartment.PrintApartmentDetails();

                                                                Console.WriteLine("\n");

                                                                currentApartment.PrintTenants();
                                                                break;
                                                            }
                                                                
                                                        }
                                                        break;
                                                    }

                                                    case "2":
                                                    {
                                                        Console.WriteLine("Choose an action - Apartment Management ");
                                                        Console.WriteLine("1. Register new tenants:\n" +
                                                                          "2. Update tenant information:\n" +
                                                                          "3. See all tenants\n" +
                                                                          "4. Remove a tenants:\n");
                                                        actionChoice = Console.ReadLine();

                                                        switch (actionChoice)
                                                        {
                                                            case "1":
                                                            {
                                                                currentApartment.AddTenant();

                                                                break;
                                                            }

                                                            case "2":
                                                            {
                                                                currentApartment.UpdateTenantInformation();

                                                                break;
                                                            }

                                                            case "3":
                                                            {
                                                                currentApartment.PrintTenants();
                                                                break;
                                                            }

                                                            case "4":
                                                            {
                                                                currentApartment.RemoveTenant();
                                                                break;
                                                            }
                                                            
                                                        }
                                                        
                                                        break;
                                                    }

                                                    case "3":
                                                    {
                                                        Console.WriteLine("Choose an action - Apartment Management ");
                                                        Console.WriteLine("1. Add a Landlord\n" +
                                                                          "2. Delete LandLord\n" +
                                                                          "3. Exit");
                                                        actionChoice = Console.ReadLine();

                                                        switch (actionChoice)
                                                        {
                                                            case "1":
                                                            {
                                                                currentApartment.AddLandlord();
                                                                break;
                                                            }

                                                            case "2":
                                                            {
                                                                LandLord.RemoveLandlordFromApartment(currentApartment);                          
                                                                break;
                                                            }
                                                        }
                                                        
                                                        break;
                                                    }

                                                    case "4":
                                                    {
                                                        Console.WriteLine("Choose an action - Apartment Management ");
                                                        Console.WriteLine("1. Add utility costs and calculate rent for a specific month\n" +
                                                                          "2. See information about UtilityExpenses\n" +
                                                                          "3. Update Rent Date:\n" +
                                                                          "4. Add a repair expense:\n" +
                                                                          "5. Close repair expense\n");
                                                        actionChoice = Console.ReadLine();

                                                        switch (actionChoice)
                                                        {
                                                            case "1":
                                                            {
                                                                currentApartment.AddUtilityExpensesAndCalculateRent();
                                                                break;
                                                            }
                                                            
                                                            case "2":
                                                            {
                                                                currentApartment.ViewUtilityExpenses();
                                                                break;
                                                            }

                                                            case "3":
                                                            {
                                                                currentApartment.UpdateRentTermDates();
                                                                break;
                                                            }

                                                            case "4":
                                                            {
                                                                currentApartment.AddRepairExpense();

                                                                break;
                                                            }

                                                            case "5":
                                                            {
                                                                currentApartment.CloseRepairExpense();
                                                                break;
                                                            }
                                                            
                                                        }
                                                        break;
                                                    }
                                                    
                                                    
                                                    case "5":
                                                        workingWithApartment = false;

                                                        break;
                                                    default:
                                                        Console.WriteLine("Invalid choice.");
                                                        break;
                                                }
                                            } while (workingWithApartment);
                                        }
                                       
                                    


                                    break;
                                }

                                case "4":
                                {
                                    Console.WriteLine("Choose an action - Apartment Management ");
                                    Console.WriteLine("1. See all LandLords\n" +
                                                      "2. See information about LandLord\n" +
                                                      "3. Calculate a total income from landlord's apartments\n");
                                    var actionChoice = Console.ReadLine();

                                    switch (actionChoice)
                                    {
                                        case "1":
                                        {
                                            currentBuilding.FindAllLandlords();
                                            LandLord.PrintLandlordInfo(currentBuilding.LandLords);
                                            break;
                                        }

                                        case "2":
                                        {
                                            currentBuilding.FindAllLandlords();
                           
                                            LandLord.FindLandlordInfoByName(currentBuilding.LandLords);
                                            break;
                                        }
                                        
                                        case "3":
                                        {
                                            LandLord.CalculateRentIncome(currentBuilding);                                    
                                            break;
                                        }
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

                case "3":
                {
                    Building.PrintAllBuildings(buildings);
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