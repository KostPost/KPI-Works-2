using Laba6;

namespace Laba6;

class Program
{
    static void Main()
    {
        Expression[] expressions = new Expression[3];
        expressions[0] = new Expression(1, 5, 3, 2);
        expressions[1] = new Expression(2, 4, 2, 1);
        expressions[2] = new Expression(3, 7, 1, 4);

        foreach (Expression expr in expressions)
        {
            try
            {
                double result = expr.Evaluate();
                Console.WriteLine("Result of the expression: " + result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in expression evaluation: " + e.Message);
            }
        }
    }
}
