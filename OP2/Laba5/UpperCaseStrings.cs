namespace Laba5;

public class UpperCaseStrings : Strings
{
    public UpperCaseStrings(string content) : base(content.ToUpper())
    {
    }

    public override void SortAndPrint()
    {
        var sorted = string.Concat(content.OrderBy(c => c));
        Console.WriteLine(sorted);
    }
}
