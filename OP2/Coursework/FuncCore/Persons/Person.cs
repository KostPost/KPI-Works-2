using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FuncCore.Buildings;

namespace FuncCore.Persons;

[Table("persons")]
public class Person
{
    [Key] public long Id { get; set; }
    [Column("name")] public string Name { get; set; }

    public PersonType PersonType { get; set; }

    [Column("apartment_id")] public long? ApartmentId { get; set; }
    [ForeignKey("ApartmentId")] public virtual Apartment Apartment { get; set; }

    public virtual ICollection<Apartment> OwnedApartments { get; set; }
}

public enum PersonType
{
    Resident,
    LandLord
}