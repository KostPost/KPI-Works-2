using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FuncCore.Buildings;

namespace FuncCore.Persons;

using System.Collections.Generic;

public class LandLord : Resident
{
    public double Income { get; set; }

    public static decimal CalculateRentIncome(List<Apartment> apartments)
    {
        decimal totalRentIncome = 0;

        foreach (var apartment in apartments)
        {
            decimal rentForMonth = apartment.CalculateRent(1);
            totalRentIncome += rentForMonth;
        }

        return totalRentIncome;
    }

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
}