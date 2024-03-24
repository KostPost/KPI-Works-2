namespace Laba5
{
    class Program
    {
        static void Main(string[] args)
        {
            Figure[] figures = new Figure[2];
            figures[0] = new Triangle(3, 4, 5);
            figures[1] = new Circle(2);

            foreach (Figure figure in figures)
            {
                Console.WriteLine($"Area: {figure.CalculateArea()}, Perimeter: {figure.CalculatePerimeter()}");
            }
        }
    }
}