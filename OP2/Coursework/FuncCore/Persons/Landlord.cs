using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FuncCore.Buildings;

namespace FuncCore.Persons;

using System.Collections.Generic;

public class LandLord : Resident
{
    public double Income { get; set; }


    public static List<Apartment> FindLandLordApartments(string? name, Building currentBuilding)
    {
        List<Apartment> landLordApartmetns = new List<Apartment>();
        foreach (var apartment in currentBuilding.Apartments)
        {
            if (apartment.ApartmentOwner.FullName == name)
            {
                landLordApartmetns.Add(apartment);
            }
        }

        return landLordApartmetns;
    }

    public static void CalculateRentIncome(List<Apartment> apartments, String landlordName)
    {
        if (apartments.Count > 0)
        {
            decimal totalRentIncome = 0;

            foreach (var apartment in apartments)
            {
                decimal rentForMonth = apartment.CalculateRent(1);
                totalRentIncome += rentForMonth;
            }

            Console.WriteLine($"Total rent income for {landlordName}: {totalRentIncome:C}");
        }
        else
        {
            Console.WriteLine($"Landlord '{landlordName}' not found.");
        }
    }

    public static void EnterLandlordNameAndGetApartments(Building currentBuilding)
    {
        Console.Write("Enter the landlord's name: ");
        string landlordName = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(landlordName))
        {
            Console.WriteLine("Landlord name cannot be empty.");
        }

        List<Apartment> landLordApartments = FindLandLordApartments(landlordName, currentBuilding);

        if (landLordApartments.Count == 0)
        {
            Console.WriteLine($"Landlord '{landlordName}' not found.");
        }

        CalculateRentIncome(landLordApartments, landlordName);
    }
}