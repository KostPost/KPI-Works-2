namespace Laba5;

public class LowerCaseStrings : Strings
{
    public LowerCaseStrings(string content) : base(content.ToLower())
    {
    }

    public override void SortAndPrint()
    {
        var sorted = string.Concat(content.OrderByDescending(c => c));
        Console.WriteLine(sorted);
    }
}
