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
    
    
    [Column("coast_by_one_m")]
    public decimal  CostByOneM { get; set; }
    
    
    [NotMapped]
    public List<Room> Rooms = new List<Room>();
    
    
    [NotMapped]
    public List<Resident> Residents { get; set; } = new List<Resident>();
}