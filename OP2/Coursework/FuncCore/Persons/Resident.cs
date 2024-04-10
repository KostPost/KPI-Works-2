namespace FuncCore.Persons;

public class Resident : Person
{
    public int ApartmentNumber { get; set; }
    public decimal RentCost { get; set; }
    public DateTime RentTermStart { get; set; }
    public DateTime RentTermEnd { get; set; }
    public decimal UtilityExpenses { get; set; }
}