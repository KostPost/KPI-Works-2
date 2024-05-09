using FuncCore;

namespace ConsoleCommander;

class Program
{
    static void Main(string[] args)
    {
        List<Building> buildings = new List<Building>();
        bool working = true;

        do
        {
            Console.WriteLine("\nElectronic directory of residents and tenants of a multi-apartment building\n" +
                              "Select an action:\n" +
                              "1: Create a building\n" +
                              "2: Select a building\n" +
                              "3. Print all Buildings\n");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                {
                    Building.CreateBuilding(buildings);
                    break;
                }

                case "2":
                {
                    if (buildings.Count == 0)
                    {
                        Console.WriteLine("There are no buildings.");
                        break;
                    }

                    Building? currentBuidling = null;
                    do
                    {
                        currentBuidling = Building.SelectBuilding(buildings);
                    } while (currentBuidling == null);


                    bool workAction = true;
                    do
                    {
                        Console.WriteLine("\nSelect an action (or type 'exit' to go back):\n" +
                                          " 1. Add apartment to the building\n" +
                                          " 2. See all apartments in the building\n" +
                                          " 3. Choose an apartment\n" +
                                          " 4. Actions with LandLords\n" +
                                          " 5. Exit\n");
                        var actionApartment = Console.ReadLine()?.ToLower();

                        switch (actionApartment)
                        {

                            case "1":
                            {
                                currentBuidling.AddApartment();
                                break;
                            }
                            
                            case "2":
                            {
                                Apartment.PrintApartmentsDetails(currentBuidling.Apartments);
                                break;
                            }

                            case "3":
                            {
                                Apartment? currentApartment = currentBuidling.GetApartmentByNumber();

                                if (currentApartment != null)
                                {
                                    bool workingApartment = true;
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
                                                bool workingInApartment = true;
                                                do
                                                {
                                                    Console.WriteLine("Choose an action - Apartment Management ");
                                                    Console.WriteLine("1. Update cost per 1 square metr\n" +
                                                                      "2. Add a Room in Apartment\n" +
                                                                      "3. View apartment information\n" +
                                                                      "4. Exit");
                                                    actionApartment = Console.ReadLine();

                                                    switch (actionApartment)
                                                    {

                                                        case "1":
                                                        {
                                                            currentApartment.UpdateCost();
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
                                                            break;
                                                        }
                                                        
                                                        case "4":
                                                            workingInApartment = false;
                                                            break;
                                                    }
                                    
                                                } while (workingInApartment);
                                                break;
                                            }

                                            case "2":
                                            {
                                                bool workingInApartment = true;
                                                do
                                                {
                                                    Console.WriteLine("Choose an action - Apartment Management ");
                                                    Console.WriteLine("1. Register new tenants:\n" +
                                                                      "2. Update tenant information:\n" +
                                                                      "3. See all tenants\n" +
                                                                      "4. Remove a tenants:\n" +
                                                                      "5. Exit\n");
                                                    actionApartment = Console.ReadLine();

                                                    switch (actionApartment)
                                                    {
                                                        case "1":
                                                        {
                                                            currentApartment.RegisterNewTenant();
                                                            break;
                                                        }

                                                        case "2":
                                                        {
                                                            currentApartment.UpdateTenantInformation();
                                                            break;
                                                        }

                                                        case "3":
                                                        {
                                                            currentApartment.DisplayAllTenants();
                                                            break;
                                                        }

                                                        case "4":
                                                        {
                                                            currentApartment.RemoveTenant();
                                                            break;
                                                        }
                                                        
                                                        case "5":
                                                            workingInApartment = false;
                                                            break;
                                                        
                                                    }
                                                } while (workingInApartment);
                                                break;
                                            }

                                            case "3":
                                            {
                                                bool workingInApartment = true;
                                                do
                                                {
                                                    Console.WriteLine("Choose an action - Apartment Management ");
                                                    Console.WriteLine("1. Add a Landlord\n" +
                                                                      "2. Delete LandLord\n" +
                                                                      "3. Exit");
                                                    actionApartment = Console.ReadLine();

                                                    switch (actionApartment)
                                                    {
                                                        case "1":
                                                        {
                                                            
                                                            break;
                                                        }
                                                        
                                                        case "3":
                                                            workingInApartment = false;
                                                            break;
                                                    }
                                                } while (workingInApartment);
                                                break;
                                            }

                                            case "5":
                                                workingApartment = false;
                                                break;
                                        }
                                    } while (workingApartment);
                                }

                                break;
                            }

                            case "5":
                                workAction = false;
                                break;
                        }
                    } while (workAction);

                    break;
                }

                case "3":
                    Building.PrintBuildings(buildings);
                    break;
            }
        } while (working);
    }
}