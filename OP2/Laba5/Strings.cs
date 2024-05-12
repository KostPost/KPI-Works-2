namespace Laba5;

using System.Linq;

public class Strings
{
    protected string content;

    public Strings(string content)
    {
        this.content = content;
    }

    public virtual int Length()
    {
        return content.Length;
    }

    public virtual void SortAndPrint()
    {
        var sorted = string.Concat(content.OrderBy(c => c));
        Console.WriteLine(sorted);
    }
}