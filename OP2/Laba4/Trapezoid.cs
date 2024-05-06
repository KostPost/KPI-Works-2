namespace Laba4;

public class Trapezoid : Figure
{
    // Конструктор з параметрами
    public Trapezoid(params (double, double)[] vertices) : base(vertices)
    {
        if (vertices.Length != 4)
            throw new ArgumentException("Trapezoid must have exactly four vertices.");
    }

    // Метод для обчислення площі трапеції
    public double Area()
    {
        // Формула площі трапеції через координати вершин
        double area = 0;
        for (int i = 0; i < vertices.Length; i++)
        {
            int next = (i + 1) % vertices.Length;
            area += vertices[i].X * vertices[next].Y - vertices[next].X * vertices[i].Y;
        }
        return Math.Abs(area / 2);
    }

    // Метод для обчислення периметра трапеції
    public double Perimeter()
    {
        double perimeter = 0;
        for (int i = 0; i < vertices.Length; i++)
        {
            int next = (i + 1) % vertices.Length;
            perimeter += SideLength(i, next);
        }
        return perimeter;
    }

    // Методи отримання даних об'єкту
    public override string ToString()
    {
        return $"Trapezoid with vertices at ({string.Join(", ", vertices)})";
    }
}