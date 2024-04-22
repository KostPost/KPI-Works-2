using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FuncCore.DataBaseActions;
using Microsoft.EntityFrameworkCore;

namespace FuncCore.Buildings;

[Index(nameof(BuildingName), IsUnique = true)]
[Table("buildings")]
public class Building
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
    public long BuildingId { get; set; }
    
    [Column("building_name")]
    public string BuildingName { get; set; }

    [Column("number_of_floors")]
    public long NumberOfFloors { get; set; }

    [NotMapped] public List<Apartment> Apartments { get; set; } = new List<Apartment>();

}