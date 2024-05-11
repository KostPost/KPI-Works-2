namespace FuncCore.Persons;

public class Tenant
{
    public string FullName { get; set; }
    public long Age { get; set; }

    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string EmergencyContact { get; set; }
    public int ApartmentNumber { get; set; }

    public static void RegisterNewTenant(Apartment apartment)
    {
        try
        {
            Console.WriteLine("Enter tenant details:");

            Console.Write("Name: ");
            var tenantName = Console.ReadLine();

            Console.Write("Age: ");
            if (!int.TryParse(Console.ReadLine(), out int tenantAge))
            {
                Console.WriteLine("Invalid input! Please enter a numeric age.");
                return;
            }

            var newTenant = new Tenant
            {
                FullName = tenantName,
                Age = tenantAge,
            };

            apartment.Tenants.Add(newTenant);
            apartment.IsOccupied = true;
            Console.WriteLine("New tenant registered successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static void RemoveTenantFromApartment(Apartment apartment)
    {
        try
        {
            Console.WriteLine("Enter the full name of the tenant to be removed:");
            var tenantName = Console.ReadLine();

            var tenant = apartment.Tenants.FirstOrDefault(t => t.FullName == tenantName);
            if (tenant == null)
            {
                Console.WriteLine("Tenant not found.");
                return;
            }

            apartment.Tenants.Remove(tenant);
            if (apartment.Tenants.Count == 0) apartment.IsOccupied = false;
            Console.WriteLine("Tenant removed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static void UpdateTenantInformation(Apartment apartment)
    {
        Console.WriteLine("Enter the tenant's full name you want to update:");
        var tenantFullName = Console.ReadLine();

        var tenant = apartment.Tenants.FirstOrDefault(t => t.FullName == tenantFullName);
        if (tenant == null)
        {
            Console.WriteLine("Tenant not found.");
            return;
        }

        Console.WriteLine($"\nCurrent Information:\nFull Name: {tenant.FullName}\nAge: {tenant.Age}\nPhone Number: " +
                          $"{tenant.PhoneNumber}\nEmail: {tenant.Email}\nEmergency Contact: {tenant.EmergencyContact}\n" +
                          $"Apartment Number: {tenant.ApartmentNumber}\n");

        Console.WriteLine("Enter new details (press enter to skip):");

        Console.Write("Full Name: ");
        var newName = Console.ReadLine();
        if (!string.IsNullOrEmpty(newName))
        {
            tenant.FullName = newName;
        }

        Console.Write("Age: ");
        if (long.TryParse(Console.ReadLine(), out long newAge))
        {
            tenant.Age = newAge;
        }

        Console.Write("Phone Number: ");
        var newPhoneNumber = Console.ReadLine();
        if (!string.IsNullOrEmpty(newPhoneNumber))
        {
            tenant.PhoneNumber = newPhoneNumber;
        }

        Console.Write("Email: ");
        var newEmail = Console.ReadLine();
        if (!string.IsNullOrEmpty(newEmail))
        {
            tenant.Email = newEmail;
        }

        Console.Write("Emergency Contact: ");
        var newEmergencyContact = Console.ReadLine();
        if (!string.IsNullOrEmpty(newEmergencyContact))
        {
            tenant.EmergencyContact = newEmergencyContact;
        }

        Console.Write("Apartment Number: ");
        if (int.TryParse(Console.ReadLine(), out int newApartmentNumber))
        {
            tenant.ApartmentNumber = newApartmentNumber;
        }

        Console.WriteLine("Tenant information updated successfully.");
    }


    public static void DisplayAllTenants(Apartment apartment)
    {
        if (apartment.Tenants.Count == 0 || !apartment.Tenants.Any())
        {
            Console.WriteLine("No tenants found.");
            return;
        }

        Console.WriteLine("All the tenants:");

        Console.WriteLine("\n-----------\n");
        foreach (var tenant in apartment.Tenants)
        {
            Console.WriteLine($"Full Name: {tenant.FullName}");
            Console.WriteLine($"Age: {tenant.Age}");
            Console.WriteLine($"Phone Number: {tenant.PhoneNumber}");
            Console.WriteLine($"Email: {tenant.Email}");
            Console.WriteLine($"Emergency Contact: {tenant.EmergencyContact}");
            Console.WriteLine($"Apartment Number: {tenant.ApartmentNumber}\n");
            Console.WriteLine("-----------");
        }
    }
}