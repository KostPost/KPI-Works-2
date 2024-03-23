using Laba3;

class Program
{
    static void Main()
    {
        Square K1 = new Square(); 
        Square K2 = new Square(2, 2, 10);
        Square K3 = new Square(K2); 

        K3 += 5;

        K1 = K2 / K3;

        Console.WriteLine(K1);
        Console.WriteLine(K2);
        Console.WriteLine(K3);
    }
}