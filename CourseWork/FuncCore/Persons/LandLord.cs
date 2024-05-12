namespace FuncCore.Persons;

public class LandLord
{
    public string FullName { get; set; }
    public List<Apartment> OwnedApartments { get; set; } = new List<Apartment>();

    public static void AddLandlordToApartment(Apartment apartment, Building building)
    {
        try
        {
            if (apartment.LandLord != null)
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

            var existingLandLord = building.LandLords.FirstOrDefault(ll =>
                ll.FullName.Equals(landlordName, StringComparison.OrdinalIgnoreCase));

            if (existingLandLord != null)
            {
                existingLandLord.OwnedApartments.Add(apartment);
                apartment.LandLord = existingLandLord;
                Console.WriteLine($"Landlord info updated successfully for {landlordName}.");
            }
            else
            {
                apartment.LandLord = new LandLord { FullName = landlordName };
                apartment.LandLord = apartment.LandLord;
                building.LandLords.Add(apartment.LandLord);
                apartment.LandLord.OwnedApartments.Add(apartment);
                Console.WriteLine($"Landlord {landlordName} added successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static void RemoveLandlordFromApartment(Apartment apartment, Building building)
    {
        try
        {
            if (apartment.LandLord == null)
            {
                Console.WriteLine("No landlord to remove.");
                return;
            }

            apartment.LandLord.OwnedApartments.Remove(apartment);

            if (apartment.LandLord.OwnedApartments.Count == 0)
            {
                building.LandLords.Remove(apartment.LandLord);
                Console.WriteLine(
                    $"Landlord {apartment.LandLord.FullName} does not own any more apartments and has been removed from the building.");
            }
            else
            {
                Console.WriteLine(
                    $"Landlord {apartment.LandLord.FullName} removed from apartment, but they still own other apartments in the building.");
            }

            apartment.LandLord = null;
            Console.WriteLine("Landlord removed from apartment successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}