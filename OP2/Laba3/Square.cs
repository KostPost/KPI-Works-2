namespace Laba3;

public class Square
{
    public double X { get; private set; }
    public double Y { get; private set; }
    public double SideLength { get; private set; }

    // Конструктор за умовчанням
    public Square()
    {
        X = 0;
        Y = 0;
        SideLength = 1; // припускаємо, що стандартний розмір сторони квадрата рівний 1
    }

    // Конструктор з параметрами
    public Square(double x, double y, double sideLength)
    {
        X = x;
        Y = y;
        SideLength = sideLength;
    }

    // Конструктор копіювання
    public Square(Square anotherSquare)
    {
        X = anotherSquare.X;
        Y = anotherSquare.Y;
        SideLength = anotherSquare.SideLength;
    }

    // Метод обчислення площі
    public double Area()
    {
        return SideLength * SideLength;
    }

    // Метод обчислення периметра
    public double Perimeter()
    {
        return 4 * SideLength;
    }

    // Перевантаження оператора додавання
    public static Square operator +(Square a, double value)
    {
        return new Square(a.X, a.Y, a.SideLength + value);
    }

    // Перевантаження оператора ділення
    public static Square operator /(Square a, Square b)
    {
        if (b.SideLength == 0)
        {
            throw new DivideByZeroException("Side length of square cannot be zero for division.");
        }

        return new Square(a.X, a.Y, a.SideLength / b.SideLength);
    }

    // Метод для отримання даних квадрата
    public override string ToString()
    {
        return $"Square: Top-Left ({X}, {Y}), Side Length: {SideLength}, Area: {Area()}, Perimeter: {Perimeter()}";
    }
}