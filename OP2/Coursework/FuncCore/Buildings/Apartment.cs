using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FuncCore.Persons;

namespace FuncCore.Buildings;

public class Apartment
{
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
    public long ApartmetId { get; set; }

    [Column("number")]
    public int Number { get; set; }
    
    [Column("room_count")]
    public long RoomCount { get; set; }
    
    [Column("floor")]
    public long Floor { get; set; }
    
    [Column("building_id")]
    public long BuildingId { get; set; }
    
    
    [Column("cost_per_square_meter")]
    public decimal? CostPerSquareMeter { get; set; }
    
    public String Resident { get; set; }
    
    [NotMapped]
    public bool IsOccupied { get; set; }
    
    
    [NotMapped]
    public List<Room> Rooms = new List<Room>();
    
    
    [NotMapped]
    public List<Resident> Residents { get; set; } = new List<Resident>();
}