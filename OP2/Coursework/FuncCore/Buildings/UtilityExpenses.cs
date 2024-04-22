using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuncCore.Buildings;

public class UtilityExpenses
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
    public long Id;
    
    [Column("apartment_id")] public long ApartmentId { get; set; }

    [Column("rent_cost")] public decimal RentCost { get; set; }
    
    [Column("all_utility_expenses")] public decimal AllUtilityExpenses { get; set; }

    [Column("heating_cost")] public decimal HeatingCost { get; set; }

    [Column("water_cost")] public decimal WaterCost { get; set; }

    [Column("electricity_cost")] public decimal ElectricityCost { get; set; }

    [Column("gas_cost")] public decimal GasCost { get; set; }

    [Column("cleaning_cost")] public decimal CleaningCost { get; set; }

    [Column("management_cost")] public decimal ManagementCost { get; set; }

    [Column("trash_removal_cost")] public decimal TrashRemovalCost { get; set; }

    [Column("internet_tv_phone_cost")] public decimal InternetTvPhoneCost { get; set; }
}