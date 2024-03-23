namespace Laba2;

public class MyString
{
    private string content;

    public MyString(string initialContent)
    {
        content = initialContent;
    }

    public void MakeFirstLetterUpperCase()
    {
        content = String.Join(" ", content.Split()
            .Select(word => Char.ToUpper(word[0]) + word.Substring(1)));
    }

    public bool Contains(string substring)
    {
        return content.Contains(substring);
    }

    public int Length => content.Length;

    public override string ToString()
    {
        return content;
    }
}