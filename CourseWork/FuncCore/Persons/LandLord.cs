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

    public static void AddLandlordToApartment(Apartment apartment,LandLord landLord, Building building)
    {
        if (landLord != null)
        {
            Console.WriteLine("A landlord already exists. Do you really want to change the landlord? (yes/no)");
            var response = Console.ReadLine();
            if (response?.ToLower() != "yes")
            {
                Console.WriteLine("Operation cancelled.");
                return;
            }
        }

        string landlordName;

        do
        {
            Console.WriteLine("Enter the full name of the new landlord to add:");
            landlordName = Console.ReadLine();
            if (string.IsNullOrEmpty(landlordName))
            {
                Console.WriteLine("The name cannot be empty. Please enter a valid name.");
            }
        } while (string.IsNullOrEmpty(landlordName));

        var existingLandLord = building.LandLords.FirstOrDefault(ll => ll.FullName.Equals(landlordName, StringComparison.OrdinalIgnoreCase));

        if (existingLandLord != null)
        {
            existingLandLord.OwnedApartments.Add(apartment);
            apartment.LandLord = existingLandLord;
            Console.WriteLine($"Landlord info updated successfully for {landlordName}.");
        }
        else
        {
            landLord = new LandLord { FullName = landlordName };
            apartment.LandLord = landLord;
            building.LandLords.Add(landLord);
            landLord.OwnedApartments.Add(apartment);
            Console.WriteLine($"Landlord {landlordName} added successfully.");
        }
    }
    
    public static void RemoveLandlordFromApartment(Apartment apartment, LandLord landLord, Building building) 
    {
        if (landLord == null) 
        {
            Console.WriteLine("No landlord to remove.");
            return;
        }

        landLord.OwnedApartments.Remove(apartment);

        if (landLord.OwnedApartments.Count == 0) 
        {
            building.LandLords.Remove(landLord);
            Console.WriteLine($"Landlord {landLord.FullName} does not own any more apartments and has been removed from the building.");
        }
        else 
        {
            Console.WriteLine($"Landlord {landLord.FullName} removed from apartment, but they still own other apartments in the building.");
        }

        apartment.LandLord = null;
        Console.WriteLine("Landlord removed from apartment successfully.");
    }
}