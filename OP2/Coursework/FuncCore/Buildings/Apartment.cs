using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FuncCore.Persons;

namespace FuncCore.Buildings;

[Table("apartments")]
public class Apartment
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
    public long ApartmetId { get; set; }

    [Column("apartment_number")] public int ApartmentNumber { get; set; }
    [Column("room_count")] public long RoomCount { get; set; }
    [Column("floor")] public long Floor { get; set; }
    [Column("building_id")] public long BuildingId { get; set; }
    [Column("cost_per_square_meter")] public decimal? CostPerSquareMeter { get; set; }
    [Column("rent_term_start")] public DateTime RentTermStart { get; set; }
    [Column("rent_term_end")] public DateTime RentTermEnd { get; set; }

    [Column("apartment_owner")] public Person ApartmentOwner = new Person();

    [ForeignKey("rooms")] public List<Room> Rooms = new List<Room>();
    [ForeignKey("residents")] public IEnumerable<Person>? Residents { get; set; } = new List<Person>();


    [NotMapped] public bool IsOccupied { get; set; }

    public void PrintApartmentDetails()
    {
        Console.WriteLine($"Apartment ID: {ApartmetId}");
        Console.WriteLine($"Apartment Number: {ApartmentNumber}");
        Console.WriteLine($"Room Count: {RoomCount}");
        Console.WriteLine($"Floor: {Floor}");
        Console.WriteLine($"Building ID: {BuildingId}");
        Console.WriteLine($"Cost Per Square Meter: {CostPerSquareMeter:C}");
        Console.WriteLine($"Is Occupied: {IsOccupied}");
        Console.WriteLine($"Rent Term Start: {RentTermStart:d}");
        Console.WriteLine($"Rent Term End: {RentTermEnd:d}");
        Console.WriteLine();

        PrintAllResidents((Residents ?? new List<Person>()).ToList());
    }

    public void PrintAllResidents(IEnumerable<Person> residents)
    {
        foreach (var resident in residents)
        {
            // Console.WriteLine($"Resident ID: {resident.Id}");
            // Console.WriteLine($"Apartment ID: {resident.ApartmentId}");
            // Console.WriteLine($"Rent Cost: {resident.UtilityExpenses.RentCost:C}");
            // Console.WriteLine($"All Utility Expenses: {resident.UtilityExpenses.AllUtilityExpenses:C}");
            // Console.WriteLine($"Rent Term Start: {RentTermStart:d}");
            // Console.WriteLine($"Rent Term End: {RentTermEnd:d}");
            // Console.WriteLine();
        }
    }
}