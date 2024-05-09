using System.Text;
using FuncCore.Persons;

namespace FuncCore;

public class Apartment
{
    public int ApartmentNumber { get; set; }
    public long RoomCount { get; set; }
    public long Floor { get; set; }
    public long BuildingId { get; set; }
    public decimal CostPerSquareMeter { get; set; }
    public DateTime RentTermStart { get; set; }
    public DateTime RentTermEnd { get; set; }
    public LandLord LandLord { get; set; }
    public List<Room> Rooms { get; set; } 
    public List<Tenant> Tenants { get; set; } 
    public List<UtilityExpense> UtilityExpenses { get; set; } 
    public List<RepairExpense> RepairExpenses { get; set; }
    public bool IsOccupied { get; set; }

    public Apartment()
    {
        Rooms = new List<Room>();
        Tenants = new List<Tenant>();
        UtilityExpenses = new List<UtilityExpense>();
        RepairExpenses = new List<RepairExpense>();
    }
    
    public void AddLandlord()
    {
        Console.WriteLine("Enter the full name of the landlord to add:");
        var landlordName = Console.ReadLine();
        
        Landlords.Add(new Landlord { FullName = landlordName });
        Console.WriteLine("Landlord added successfully.");
    }

    public void RemoveLandlord()
    {
        Console.WriteLine("Enter the full name of the landlord to remove:");
        var landlordName = Console.ReadLine();
        
        var landlord = Landlords.FirstOrDefault(l => l.FullName == landlordName);
        if (landlord == null)
        {
            Console.WriteLine("Landlord not found.");
            return;
        }

        Landlords.Remove(landlord);
        Console.WriteLine("Landlord removed successfully.");
    }
    public void RemoveTenant()
    {
        Console.WriteLine("Enter the full name of the tenant to be removed:");
        var tenantName = Console.ReadLine();
        
        var tenant = Tenants.FirstOrDefault(t => t.FullName == tenantName);
        if (tenant == null)
        {
            Console.WriteLine("Tenant not found.");
            return;
        }

        Tenants.Remove(tenant);
        Console.WriteLine("Tenant removed successfully.");
    }
    public void DisplayAllTenants()
    {
        if(Tenants.Count == 0 || !Tenants.Any())
        {
            Console.WriteLine("No tenants found.");
            return;
        }

        Console.WriteLine("All the tenants:");
        foreach (var tenant in Tenants)
        {
            Console.WriteLine("-----------");
            Console.WriteLine($"Full Name: {tenant.FullName}");
            Console.WriteLine($"Age: {tenant.Age}");
            Console.WriteLine($"Phone Number: {tenant.PhoneNumber}");
            Console.WriteLine($"Email: {tenant.Email}");
            Console.WriteLine($"Emergency Contact: {tenant.EmergencyContact}");
            Console.WriteLine($"Apartment Number: {tenant.ApartmentNumber}\n");
            Console.WriteLine("-----------");

        }
    }
    public void UpdateTenantInformation()
    {
        Console.WriteLine("Enter the tenant's full name you want to update:");
        var tenantFullName = Console.ReadLine();
        
        var tenant = Tenants.FirstOrDefault(t => t.FullName == tenantFullName);
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
    public void RegisterNewTenant()
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

        Tenants.Add(newTenant);
        Console.WriteLine("New tenant registered successfully.");
    }
    public void AddRoom()
    {
        Console.WriteLine("Enter room details:");

        int roomNumber;
        while (true)
        {
            Console.Write("Room Number: ");
            if (int.TryParse(Console.ReadLine(), out roomNumber) && roomNumber <= RoomCount)
            {
                break;
            }
            else
            {
                Console.WriteLine($"Invalid input. Please enter a valid room number not greater than {RoomCount}.");
            }
        }

        double roomArea;
        while (true)
        {
            Console.Write("Room Area: ");
            if (double.TryParse(Console.ReadLine(), out roomArea))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid room area.");
            }
        }

        var room = new Room
        {
            RoomNumber = roomNumber,
            Area = roomArea
        };

        Rooms.Add(room);
        Console.WriteLine("Room added successfully.");
    }
    public void UpdateCost()
    {
        Console.WriteLine("Enter the new cost per one metr:");
        if (decimal.TryParse(Console.ReadLine(), out decimal newCost))
        {
            CostPerSquareMeter = newCost;
            Console.WriteLine("Cost updated successfully.");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid decimal number.");
        }
    }
    
    public void PrintApartmentDetails()
    {
        Console.WriteLine("-----------");

        var builder = new StringBuilder();
        builder.AppendLine($"Apartment Number: {ApartmentNumber}");
        builder.AppendLine($"Room Count: {RoomCount}");
        builder.AppendLine($"Floor: {Floor}");
        builder.AppendLine($"Building ID: {BuildingId}");
        builder.AppendLine($"Cost Per Square Meter: {CostPerSquareMeter:C}");
        builder.AppendLine($"Rent Term Start: {RentTermStart:d}");
        builder.AppendLine($"Rent Term End: {RentTermEnd:d}");
        builder.AppendLine($"Is Occupied: {IsOccupied}");

        if (ApartmentOwner != null)
        {
            builder.AppendLine("Apartment Owner:");
            builder.AppendLine($"  Name: {ApartmentOwner.FullName}");
        }

        if (Tenants?.Any() == true)
        {
            builder.AppendLine("Tenants:");
            foreach(var tenant in Tenants)
            {
                builder.AppendLine($"  Name: {tenant.FullName}");
            }
        }

        if(UtilityExpenses.Count != 0)
        {
            UtilityExpense.PrintUtilityExpenses(UtilityExpenses);
        }

        if(RepairExpenses.Count != 0)
        {
            RepairExpense.PrintRepairExpenses(RepairExpenses);
        }

        Console.Write(builder.ToString());
        Console.WriteLine("-----------");

    }
    public static void PrintApartmentsDetails(List<Apartment> apartments)
    {
        foreach (var apartment in apartments)
        {
            Console.WriteLine("-----------");

            var builder = new StringBuilder();
        
            builder.AppendLine($"Apartment Number: {apartment.ApartmentNumber}");
            builder.AppendLine($"Room Count: {apartment.RoomCount}");
            builder.AppendLine($"Floor: {apartment.Floor}");
            builder.AppendLine($"Building ID: {apartment.BuildingId}");
            builder.AppendLine($"Cost Per Square Meter: {apartment.CostPerSquareMeter:C}");
            builder.AppendLine($"Rent Term Start: {apartment.RentTermStart:d}");
            builder.AppendLine($"Rent Term End: {apartment.RentTermEnd:d}");
            builder.AppendLine($"Is Occupied: {apartment.IsOccupied}");

            if (apartment.ApartmentOwner != null)
            {
                builder.AppendLine("Apartment Owner:");
                builder.AppendLine($"  Name: {apartment.ApartmentOwner.FullName}");
            }

            if (apartment.Tenants?.Any() == true)
            {
                builder.AppendLine("Tenants:");
                foreach(var tenant in apartment.Tenants)
                {
                    builder.AppendLine($"  Name: {tenant.FullName}");
                }
            }

            if(apartment.UtilityExpenses.Count != 0)
            {
                UtilityExpense.PrintUtilityExpenses(apartment.UtilityExpenses);
            }

            if(apartment.RepairExpenses.Count != 0)
            {
                RepairExpense.PrintRepairExpenses(apartment.RepairExpenses);
            }

            Console.Write(builder.ToString());
            Console.WriteLine("-----------");
        }
    }
    
    
    

    

}