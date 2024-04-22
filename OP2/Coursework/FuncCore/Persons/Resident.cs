// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
// using FuncCore.Buildings;
//
// namespace FuncCore.Persons;
//
// public class Resident : Person
// {
//     [Column("apartment_id")] public long? ApartmentId { get; set; } // Теперь nullable
//
//     [ForeignKey("ApartmentId")]
//     public virtual Apartment Apartment { get; set; }
// }
