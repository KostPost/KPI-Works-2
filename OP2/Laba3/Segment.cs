namespace Laba3;

public class Segment
{
    public double X1 { get; private set; }
    public double Y1 { get; private set; }
    public double X2 { get; private set; }
    public double Y2 { get; private set; }

    public Segment()
    {
        X1 = Y1 = X2 = Y2 = 0.0;
    }

    public Segment(double x1, double y1, double x2, double y2)
    {
        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;
    }

    public Segment(Segment other)
    {
        X1 = other.X1;
        Y1 = other.Y1;
        X2 = other.X2;
        Y2 = other.Y2;
    }

    public double Length()
    {
        return Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
    }

    public static Segment operator +(Segment a, Segment b)
    {
        return new Segment(a.X1 + b.X1, a.Y1 + b.Y1, a.X2 + b.X2, a.Y2 + b.Y2);
    }

    public static Segment operator -(Segment a, Segment b)
    {
        return new Segment(a.X1 - b.X1, a.Y1 - b.Y1, a.X2 - b.X2, a.Y2 - b.Y2);
    }

    public override string ToString()
    {
        return $"Segment: Start({X1}, {Y1}), End({X2}, {Y2})";
    }
}