using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using FuncCore.Buildings;

namespace FuncCore.Persons;

using System.Collections.Generic;

public class LandLord 
{
    public string FullName { get; set; }
    
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string EmergencyContact { get; set; }
    public int ApartmentNumber { get; set; }

    public List<Apartment> OwnedApartments { get; set; } = new List<Apartment>();
    public double Income { get; set; }
    
    public static void FindLandlordInfoByName(List<LandLord> landlords)
    {
        var landlord = FindLandlordByName(landlords);
        if (landlord != null)
        {
            Console.WriteLine($"Full Name: {landlord.FullName}");
            Console.WriteLine($"Phone Number: {landlord.PhoneNumber}");
            Console.WriteLine($"Email: {landlord.Email}");
            Console.WriteLine($"Emergency Contact: {landlord.EmergencyContact}");
            Console.WriteLine($"Apartment Number: {landlord.ApartmentNumber}");
            Console.WriteLine($"Income: {landlord.Income}");
            Console.WriteLine($"Owned Apartments: {landlord.OwnedApartments.Count}");

            foreach (var apartment in landlord.OwnedApartments)
            {
                Console.WriteLine($"  Apartment Number: {apartment.ApartmentNumber}");
                Console.WriteLine($"  Building ID: {apartment.BuildingId}");
                Console.WriteLine($"  Room Count: {apartment.RoomCount}");
                Console.WriteLine($"  Floor: {apartment.Floor}");
                Console.WriteLine($"  Cost Per Square Meter: {apartment.CostPerSquareMeter}");
                Console.WriteLine($"  Rent Term Start: {apartment.RentTermStart}");
                Console.WriteLine($"  Rent Term End: {apartment.RentTermEnd}");
                Console.WriteLine($"  Is Occupied: {apartment.IsOccupied}");
            }
        }
        else
        {
            Console.WriteLine($"Landlord with name this name not found.");
        }
    }
    
    private static LandLord? FindLandlordByName(List<LandLord> landlords)
    {
        Console.Write("Enter the landlord's name: ");
        string name = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Landlord name cannot be empty.");
            return null;
        }

        foreach (var landLord in landlords)
        {
            Console.WriteLine(landLord.FullName);
        }
        
        return landlords.FirstOrDefault(landlord => landlord.FullName == name);
    }
    
    public static void RemoveLandlordFromApartment(Apartment currentApartment)
    {
        try
        {
            if (currentApartment.ApartmentOwner is LandLord landlord)
            {
                Console.WriteLine($"Current landlord for apartment {currentApartment.ApartmentNumber}: {landlord.FullName}");
                Console.Write("Are you sure you want to remove this landlord from the apartment? (yes/no): ");

                string choice = Console.ReadLine().Trim().ToLower();

                if (choice == "yes")
                {
                    currentApartment.ApartmentOwner = null;
                    Console.WriteLine($"Landlord '{landlord.FullName}' removed from apartment {currentApartment.ApartmentNumber}.");
                }
                else
                {
                    Console.WriteLine("Landlord removal cancelled.");
                }
            }
            else
            {
                Console.WriteLine($"There is no landlord assigned to apartment {currentApartment.ApartmentNumber}.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }


  
    
    

    private static List<Apartment> FindLandLordApartments(string? name, Building currentBuilding)
    {
        try
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Landlord name cannot be null.");
            }

            List<Apartment> landLordApartments = new List<Apartment>();
            foreach (var apartment in currentBuilding.Apartments)
            {
                if (apartment.ApartmentOwner is LandLord landlord && landlord.FullName == name)
                {
                    landLordApartments.Add(apartment);
                }
            }

            return landLordApartments;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return new List<Apartment>(); 
        }
    }

       public static void CalculateRentIncome(Building currentBuilding)
    {
        try
        {
            Console.Write("Enter the landlord's name: ");
            string landlordName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(landlordName))
            {
                Console.WriteLine("Landlord name cannot be empty.");
                return;
            }

            List<Apartment> apartments = FindLandLordApartments(landlordName, currentBuilding);

            Console.Write("Enter the start date of the period (yyyy-MM-dd): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
            {
                Console.WriteLine("Invalid date format. Please enter the date in yyyy-MM-dd format.");
                return;
            }

            Console.Write("Enter the end date of the period (yyyy-MM-dd): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
            {
                Console.WriteLine("Invalid date format. Please enter the date in yyyy-MM-dd format.");
                return;
            }

            CalculateRentIncome(apartments, landlordName, startDate, endDate);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static void CalculateRentIncome(List<Apartment> apartments, string landlordName, DateTime startDate, DateTime endDate)
    {
        try
        {
            if (apartments.Count == 0)
            {
                Console.WriteLine($"Landlord '{landlordName}' not found or has no apartments.");
                return;
            }

            decimal totalRentIncome = 0;
            foreach (var apartment in apartments)
            {
                if (apartment.ApartmentOwner is not LandLord || (apartment.ApartmentOwner is LandLord landlord && landlord.ApartmentNumber != apartment.ApartmentNumber))
                {
                    decimal rentForPeriod = apartment.CalculateRentForPeriod(startDate, endDate);
                    totalRentIncome += rentForPeriod;
                }
            }

            Console.WriteLine($"Total rent income for {landlordName} from {startDate:d} to {endDate:d}: {totalRentIncome:C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    
    
    
   

    
    
    public static void PrintLandlordInfo(List<LandLord> landLords)
    {
        foreach (var landlord in landLords)
        {
            Console.WriteLine("\n");
            Console.WriteLine($"Full Name: {landlord.FullName}");
            Console.WriteLine($"Phone Number: {landlord.PhoneNumber}");
            Console.WriteLine($"Email: {landlord.Email}");
            Console.WriteLine($"Emergency Contact: {landlord.EmergencyContact}");
            Console.WriteLine($"Apartment Number: {landlord.ApartmentNumber}");
            Console.WriteLine($"Income: {landlord.Income}");
            Console.WriteLine($"Owned Apartments: {landlord.OwnedApartments.Count}");

            foreach (var apartment in landlord.OwnedApartments)
            {
                Console.WriteLine($"  Apartment Number: {apartment.ApartmentNumber}");
                Console.WriteLine($"  Building ID: {apartment.BuildingId}");
                Console.WriteLine($"  Room Count: {apartment.RoomCount}");
                Console.WriteLine($"  Floor: {apartment.Floor}");
                Console.WriteLine($"  Cost Per Square Meter: {apartment.CostPerSquareMeter}");
                Console.WriteLine($"  Rent Term Start: {apartment.RentTermStart}");
                Console.WriteLine($"  Rent Term End: {apartment.RentTermEnd}");
                Console.WriteLine($"  Is Occupied: {apartment.IsOccupied}");
                Console.WriteLine("\n");
            }
        }
    }
}