using Laba6;

class Program
{
    static void Main(string[] args)
    {
        // Create an array of Expression objects with example values
        Expression[] expressions = new Expression[]
        {
            new Expression(1, 2, 3), // Make sure 2*c - a is positive
            new Expression(1, 3, 1), // Likewise
            new Expression(1, 4, 0)
            // ... Add more expressions with different values if needed
        };

        // Compute and print the result for each expression
        for (int i = 0; i < expressions.Length; i++)
        {
            double result = expressions[i].Compute();
            if (!double.IsNaN(result))
            {
                Console.WriteLine($"Expression {i+1} result: {result}\n");
            }
            else
            {
                Console.WriteLine($"Expression {i+1} could not be computed due to an error.\n");
            }
        }
    }
}
