using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FuncCore.Persons;

namespace FuncCore.Buildings;

[Table("rooms")]
public class Room
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
    public long RoomId { get; set; }

    [Column("room_number")]
    public int RoomNumber { get; set; }

    [Column("area")]
    public double Area { get; set; }

    [Column("apartment_id")]
    public long ApartmentId { get; set; }

}
