using System.ComponentModel.DataAnnotations.Schema;
using FuncCore.Buildings;
using FuncCore.DataBaseActions.Buildings;
using FuncCore.Persons;
using Microsoft.EntityFrameworkCore;

namespace FuncCore.DataBaseActions.Persons;

public class PersonContext : DbContext
{
    
    public DbSet<Person> Persons { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=\"Management of the house\";Username=postgres;Password=2025");
    }
    
    
    public static List<Person>? FindResidentsByApartmentId(PersonContext context, long id)
    {
        var persons = context.Persons.Where(a => a.ApartmentId == id).ToList();
        return persons.Count > 0 ? persons : null;
    }
    
    public static void AddResident(PersonContext context, string name,long apartmentId)
    {
        var residentExists = context.Persons.Any(p => p.ApartmentId == apartmentId);
        if (residentExists)
        {
            Console.WriteLine("Error: An apartment cannot have more than one resident.");
            return;
        }

        var newResident = new Person()
        {
            Name = name,
            ApartmentId = apartmentId,
            PersonType = PersonType.Resident
        };

        context.Persons.Add(newResident);
        context.SaveChanges();
        Console.WriteLine($"Resident {newResident.Name} added successfully to apartment ID {newResident.ApartmentId}.");
    }

    
}