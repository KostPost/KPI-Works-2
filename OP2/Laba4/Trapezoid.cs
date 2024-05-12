namespace Laba4;

public class Trapezoid : Figure
{
    public Trapezoid(params (double, double)[] vertices) : base(vertices)
    {
        if (vertices.Length != 4)
            throw new ArgumentException("Trapezoid must have exactly four vertices.");
    }

    public double Area()
    {
        double area = 0;
        for (int i = 0; i < vertices.Length; i++)
        {
            int next = (i + 1) % vertices.Length;
            area += vertices[i].X * vertices[next].Y - vertices[next].X * vertices[i].Y;
        }
        return Math.Abs(area / 2);
    }

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

    public override string ToString()
    {
        return $"Trapezoid with vertices at ({string.Join(", ", vertices)})";
    }
}