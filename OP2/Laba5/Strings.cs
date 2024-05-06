namespace Laba5;

using System.Linq;

public class Strings
{
    protected string content;

    public Strings(string content)
    {
        this.content = content;
    }

    // Віртуальний метод для обчислення довжини рядка
    public virtual int Length()
    {
        return content.Length;
    }

    // Віртуальний метод для сортування і виведення рядка
    public virtual void SortAndPrint()
    {
        var sorted = string.Concat(content.OrderBy(c => c));
        Console.WriteLine(sorted);
    }
}