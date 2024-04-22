// using FuncCore.Buildings;
// using FuncCore.DataBaseActions.Buildings;
// using FuncCore.Persons;
// using Microsoft.EntityFrameworkCore;
//
// namespace FuncCore.DataBaseActions.Persons;
// public class ResidentContext : DbContext
// {
//     public DbSet<Resident> residents { get; set; }
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseNpgsql("Host=localhost;Database=\"Management of the house\";Username=postgres;Password=2025");
//     }
//
//     public static List<Resident>? FindResidentById(ResidentContext context, long id)
//     {
//         var residents = context.residents.Where(a => a.ApartmentId == id).ToList();
//         return residents.Count > 0 ? residents : null;
//     }
//     
//     public static void AddResident(ResidentContext context, string name, long apartmentId)
//     {
//         var apartmentExists = ApartmentContext.FindApartmentById(new ApartmentContext(),apartmentId);
//         if (apartmentExists == null)
//         {
//             Console.WriteLine("Error: The specified apartment does not exist.");
//             return;
//         }
//
//         var newResident = new Resident
//         {
//             Name = name,
//             ApartmentId = apartmentId
//         };
//
//         context.residents.Add(newResident);
//         context.SaveChanges();
//         Console.WriteLine($"Resident {newResident.Name} added successfully to apartment ID {newResident.ApartmentId}.");
//     }
// }