namespace Laba6;

public class Expression
{
    private double a;
    private double c;
    private double d;

    public Expression(double a = 0, double c = 0, double d = 0)
    {
        this.a = a;
        this.c = c;
        this.d = d;
    }

    public void SetA(double a)
    {
        this.a = a;
    }

    public void SetC(double c)
    {
        this.c = c;
    }

    public void SetD(double d)
    {
        this.d = d;
    }

    public double Compute()
    {
        try
        {
            if (2 * c - a <= 0)
            {
                throw new ArgumentException("Logarithm base must be positive.");
            }

            double denominator = a / 4 + c;
            if (denominator == 0)
            {
                throw new DivideByZeroException("Denominator cannot be zero.");
            }

            return (Math.Log10(2 * c - a) + d - 152) / denominator;
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }

        return double.NaN; 
    }
}