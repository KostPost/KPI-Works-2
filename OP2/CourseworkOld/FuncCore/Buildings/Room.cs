using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FuncCore.Persons;

namespace FuncCore.Buildings;

public class Room
{
    public int RoomNumber { get; set; }

    public double Area { get; set; }

    public long ApartmentId { get; set; }
}
