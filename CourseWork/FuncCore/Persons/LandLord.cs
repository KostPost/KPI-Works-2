namespace FuncCore.Persons;

public class LandLord
{
    public string FullName { get; set; }
    
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string EmergencyContact { get; set; }
    public int ApartmentNumber { get; set; }

    public List<Apartment> OwnedApartments { get; set; } = new List<Apartment>();
    public double Income { get; set; }
}