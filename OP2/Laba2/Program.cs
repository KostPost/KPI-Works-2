using Laba2;

class Program
{
    static void Main()
    {
        var textContainer = new TextContainer();

        textContainer.AddLine(new MyString("hello world"));
        textContainer.AddLine(new MyString("the quick brown fox"));
        textContainer.AddLine(new MyString("jumps over the lazy dog"));

        Console.WriteLine("Initial text:");
        Console.WriteLine(textContainer);

        Console.WriteLine("\nText after capitalizing first letters:");
        textContainer.CapitalizeFirstLetters();
        Console.WriteLine(textContainer);

        Console.WriteLine("\nLongest line:");
        Console.WriteLine(textContainer.GetLongestLine());

        Console.WriteLine("\nText after removing lines containing 'the':");
        textContainer.RemoveLinesContaining("he");
        Console.WriteLine(textContainer);

        Console.WriteLine("\nText after clearing:");
        textContainer.ClearText();
        Console.WriteLine(textContainer);
    }
}