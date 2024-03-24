namespace Laba5;

class Triangle : Figure
{
    private double[] sides = new double[3];

    public Triangle(double side1, double side2, double side3)
    {
        sides[0] = side1;
        sides[1] = side2;
        sides[2] = side3;
    }

    public override double CalculateArea()
    {
        double s = (sides[0] + sides[1] + sides[2]) / 2;
        return Math.Sqrt(s * (s - sides[0]) * (s - sides[1]) * (s - sides[2]));
    }

    public override double CalculatePerimeter()
    {
        return sides[0] + sides[1] + sides[2];
    }
}
