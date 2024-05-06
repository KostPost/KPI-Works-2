using Laba4;


namespace Laba4;

class Program
{
    static void Main()
    {
        var trapezoid = new Trapezoid((1, 1), (4, 1), (5, 5), (2, 5));

        Console.WriteLine(trapezoid);
        Console.WriteLine("Area: " + trapezoid.Area());
        Console.WriteLine("Perimeter: " + trapezoid.Perimeter());
    }
}