namespace Laba6;

using System;

public class Expression
{
    private double a;
    private double b;
    private double c;
    private double d;

    // Конструктор за умовчанням
    public Expression()
    {
    }

    // Конструктор з параметрами
    public Expression(double a, double b, double c, double d)
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
    }

    public void SetA(double a) { this.a = a; }
    public void SetB(double b) { this.b = b; }
    public void SetC(double c) { this.c = c; }
    public void SetD(double d) { this.d = d; }

    public double Evaluate()
    {
        if (d == 0) throw new DivideByZeroException("Division by zero in the denominator.");
        if (4 * b - c <= 0) throw new ArgumentException("Invalid argument for logarithm.");
        double numerator = Math.Log10(4 * b - c) * a;
        double denominator = b + c / d - 1;
        if (denominator == 0) throw new DivideByZeroException("Denominator evaluates to zero.");

        return numerator / denominator;
    }
}
