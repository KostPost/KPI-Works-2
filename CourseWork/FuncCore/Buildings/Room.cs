namespace FuncCore;

public class Room
{
    public int RoomNumber { get; set; }
    public double Area { get; set; }

    public static void AddRoom(Apartment apartment)
    {
        try
        {
            Console.WriteLine("Enter room details:");

            int roomNumber;
            while (true)
            {
                Console.Write("Room Number: ");
                if (int.TryParse(Console.ReadLine(), out roomNumber) && roomNumber <= apartment.RoomCount)
                {
                    break;
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
            }

            var room = new Room
            {
                RoomNumber = roomNumber,
                Area = roomArea
            };

            apartment.Rooms.Add(room);
            Console.WriteLine("Room added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}