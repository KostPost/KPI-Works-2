using Laba2;

class Program
{
    static void Main(string[] args)
    {
        Text text1 = new Text();
        text1.AddLine("Hello, world!");
        text1.AddLine("This is a sample text with multiple spaces.");
        text1.AddLine("  This line has extra spaces at the beginning and end.   ");
        text1.Display();

        Console.WriteLine($"Shortest line length: {text1.ShortestLineLength()}");
        Console.WriteLine($"Consonant percentage: {text1.ConsonantPercentage()}%");
        text1.ReplaceMultipleSpaces();
        text1.TrimAndNormalize();
        Console.WriteLine("Text after replacing multiple spaces and trimming:");
        text1.Display();
    }
}