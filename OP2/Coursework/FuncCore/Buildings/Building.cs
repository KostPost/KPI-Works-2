using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FuncCore.Buildings;

public class Building
{
    public long BuildingId;
    public string BuildingName { get; set; }

    public long NumberOfFloors { get; set; }
    
    public List<Apartment> Apartments { get; set; } = new List<Apartment>();
    
    private static long lastBuildingId = 0;


    public Building(string buildingName, long numberOfFloors)
    {
        BuildingId = GenerateBuildingId();
        BuildingName = buildingName;
        NumberOfFloors = numberOfFloors;
    }
    private static long GenerateBuildingId()
    {
        return ++lastBuildingId;
    }
    
    public bool AddApartment(Apartment newApartment)
    {
        if (Apartments.Any(apartment => apartment.ApartmentNumber == newApartment.ApartmentNumber))
        {
            Console.WriteLine($"Error: Apartment with number {newApartment.ApartmentNumber} already exists in building {BuildingName}.");
            return false;
        }

        if (newApartment.Floor > NumberOfFloors)
        {
            Console.WriteLine("Error: The floor of the apartment cannot exceed the maximum floor of the building.");
            return false;
        }

        Apartments.Add(newApartment);
        return true;
    }

}